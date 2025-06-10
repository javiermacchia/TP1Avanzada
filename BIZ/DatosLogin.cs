using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BIZ
{
    public class DatosLogin
    {
        public struct StUsuario
        {
            public int UserId;
            public string Username;
            public string Password;
            public string Email;
            public byte PermissionLevel;
            public DateTime CreatedAt;
        }

        /// <summary>
        /// Lee la cadena de conexión "SQLCLASE" del Web.config.
        /// </summary>
        private static string GetConnectionString() =>
            ConfigurationManager
                .ConnectionStrings["SQLCLASE"]
                .ConnectionString;

        /// <summary>
        /// Devuelve 1 si existe un usuario con esas credenciales, 0 en caso contrario.
        /// </summary>
        public static int LoginUsuario(string usuario, string password)
        {
            string sql = $@"
                SELECT ISNULL(COUNT(UserId), 0)
                  FROM Users
                 WHERE Username = '{usuario}'
                   AND Password = '{password}'";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Si el usuario/contraseña coincide, devuelve un StUsuario con todos sus datos;
        /// si no, devuelve un struct con UserId = 0.
        /// </summary>
        public static StUsuario LoginUsuarioII(string usuario, string password)
        {
            var u = new StUsuario();

            string sql = $@"
                SELECT 
                    UserId,
                    Username,
                    Password,
                    Email,
                    PermissionLevel,
                    CreatedAt
                  FROM Users
                 WHERE Username = '{usuario}'
                   AND Password = '{password}'";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        u.UserId = (int)rdr["UserId"];
                        u.Username = rdr["Username"].ToString();
                        u.Password = rdr["Password"].ToString();
                        u.Email = rdr["Email"].ToString();
                        u.PermissionLevel = (byte)rdr["PermissionLevel"];
                        u.CreatedAt = (DateTime)rdr["CreatedAt"];
                    }
                }
            }

            return u;
        }

        /// <summary>
        /// Recupera los datos completos del usuario dado su UserId.
        /// </summary>
        public static StUsuario GetUsuario(int userId)
        {
            var u = new StUsuario();

            string sql = @"
                SELECT 
                    UserId,
                    Username,
                    Password,
                    Email,
                    PermissionLevel,
                    CreatedAt
                  FROM Users
                 WHERE UserId = @id";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        u.UserId = (int)rdr["UserId"];
                        u.Username = rdr["Username"].ToString();
                        u.Password = rdr["Password"].ToString();
                        u.Email = rdr["Email"].ToString();
                        u.PermissionLevel = (byte)rdr["PermissionLevel"];
                        u.CreatedAt = (DateTime)rdr["CreatedAt"];
                    }
                }
            }

            return u;
        }

        /// <summary>
        /// Actualiza Username, Email y Password de un usuario.
        /// Devuelve true si la actualización afectó al menos una fila.
        /// </summary>
        public static bool UpdateUsuario(int userId, string username, string email, string password)
        {
            string sql = @"
                UPDATE Users
                   SET Username = @user,
                       Email    = @mail,
                       Password = @pwd
                 WHERE UserId   = @id";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@id", userId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
