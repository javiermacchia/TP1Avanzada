using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BIZ
{
    public static class PasswordResetService
    {
        private static string ConnString =>
            ConfigurationManager
                .ConnectionStrings["SQLCLASE"]
                .ConnectionString;

        /// <summary>
        /// Genera un token, lo guarda con 20 min de validez y devuelve el GUID.
        /// Lanza excepción si el email no existe.
        /// </summary>
        public static Guid CreateToken(string email)
        {
            // 1) Busca userId por email
            int userId;
            const string q1 = "SELECT UserId FROM Users WHERE Email = @mail";
            using (var cx = new SqlConnection(ConnString))
            using (var cm = new SqlCommand(q1, cx))
            {
                cm.Parameters.Add("@mail", SqlDbType.NVarChar, 256).Value = email;
                cx.Open();
                var o = cm.ExecuteScalar();
                if (o == null)
                    throw new Exception("No existe usuario con ese email.");
                userId = (int)o;
            }

            // 2) Inserta token
            var token = Guid.NewGuid();
            var exp = DateTime.UtcNow.AddMinutes(20);
            const string q2 = @"
                INSERT INTO PasswordResetTokens
                        (Token, UserId, Expiration, Used)
                VALUES (@t, @u, @e, 0)";
            using (var cx = new SqlConnection(ConnString))
            using (var cm = new SqlCommand(q2, cx))
            {
                cm.Parameters.Add("@t", SqlDbType.UniqueIdentifier).Value = token;
                cm.Parameters.Add("@u", SqlDbType.Int).Value = userId;
                cm.Parameters.Add("@e", SqlDbType.DateTime).Value = exp;
                cx.Open();
                cm.ExecuteNonQuery();
            }

            return token;
        }

        /// <summary>
        /// Valida que el token exista, no esté usado y no haya expirado.
        /// NO lo marca como usado.
        /// </summary>
        public static bool ValidateToken(Guid token)
        {
            const string q = @"
                SELECT Expiration, Used
                  FROM PasswordResetTokens
                 WHERE Token = @t";
            using (var cx = new SqlConnection(ConnString))
            using (var cm = new SqlCommand(q, cx))
            {
                cm.Parameters.Add("@t", SqlDbType.UniqueIdentifier).Value = token;
                cx.Open();
                using (var r = cm.ExecuteReader())
                {
                    if (!r.Read()) return false;
                    if (r.GetBoolean(1)) return false;  // Used = 1
                    if (r.GetDateTime(0) < DateTime.UtcNow) return false;  // expirado
                    return true;
                }
            }
        }

        /// <summary>
        /// Cambia la contraseña (texto plano) del usuario asociado al token,
        /// marca el token como usado y devuelve true si tuvo éxito.
        /// </summary>
        public static bool ResetPassword(Guid token, string newPassword)
        {
            // 1) Obtiene userId y chequea validez
            int userId;
            const string q1 = @"
                SELECT UserId, Used, Expiration
                  FROM PasswordResetTokens
                 WHERE Token = @t";
            using (var cx = new SqlConnection(ConnString))
            using (var cm = new SqlCommand(q1, cx))
            {
                cm.Parameters.Add("@t", SqlDbType.UniqueIdentifier).Value = token;
                cx.Open();
                using (var r = cm.ExecuteReader())
                {
                    if (!r.Read()) return false;
                    if (r.GetBoolean(1)) return false;
                    if (r.GetDateTime(2) < DateTime.UtcNow) return false;
                    userId = r.GetInt32(0);
                }
            }

            // 2) En la misma conexión, en transacción: actualiza Users y marca el token
            using (var cx = new SqlConnection(ConnString))
            {
                cx.Open();
                using (var tx = cx.BeginTransaction())
                {
                    try
                    {
                        // a) actualizar contraseña
                        const string updUser = @"
                            UPDATE Users
                               SET Password = @pwd
                             WHERE UserId = @u";
                        using (var cm1 = new SqlCommand(updUser, cx, tx))
                        {
                            cm1.Parameters.Add("@pwd", SqlDbType.NVarChar, 512).Value = newPassword;
                            cm1.Parameters.Add("@u", SqlDbType.Int).Value = userId;
                            cm1.ExecuteNonQuery();
                        }

                        // b) marcar token usado
                        const string updToken = @"
                            UPDATE PasswordResetTokens
                               SET Used = 1
                             WHERE Token = @t";
                        using (var cm2 = new SqlCommand(updToken, cx, tx))
                        {
                            cm2.Parameters.Add("@t", SqlDbType.UniqueIdentifier).Value = token;
                            cm2.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return true;
                    }
                    catch
                    {
                        tx.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
