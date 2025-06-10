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
            public string FirstName;       // Nuevo
            public string LastName;        // Nuevo
            public byte PermissionLevel;
            public DateTime CreatedAt;
        }

        private static string GetConnectionString() =>
            ConfigurationManager
                .ConnectionStrings["SQLCLASE"]
                .ConnectionString;

        /// <summary>
        /// Devuelve 1 si existe un usuario con esas credenciales, 0 en caso contrario.
        /// </summary>
        public static int LoginUsuario(string usuario, string password)
        {
            const string sql = @"
                SELECT COUNT(*) 
                  FROM Users
                 WHERE Username = @user
                   AND Password = @pwd";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pwd", password);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Si coincide, devuelve un StUsuario poblado; si no, UserId = 0.
        /// </summary>
        public static StUsuario LoginUsuarioII(string usuario, string password)
        {
            var u = new StUsuario();
            const string sql = @"
                SELECT 
                    UserId,
                    Username,
                    Password,
                    Email,
                    FirstName,
                    LastName,
                    PermissionLevel,
                    CreatedAt
                  FROM Users
                 WHERE Username = @user
                   AND Password = @pwd";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pwd", password);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        u.UserId = (int)rdr["UserId"];
                        u.Username = rdr["Username"].ToString();
                        u.Password = rdr["Password"].ToString();
                        u.Email = rdr["Email"].ToString();
                        u.FirstName = rdr["FirstName"].ToString();
                        u.LastName = rdr["LastName"].ToString();
                        u.PermissionLevel = (byte)rdr["PermissionLevel"];
                        u.CreatedAt = (DateTime)rdr["CreatedAt"];
                    }
                }
            }

            return u;
        }

        /// <summary>
        /// Recupera todos los datos del usuario dado su UserId.
        /// </summary>
        public static StUsuario GetUsuario(int userId)
        {
            var u = new StUsuario();
            const string sql = @"
                SELECT 
                    UserId,
                    Username,
                    Password,
                    Email,
                    FirstName,
                    LastName,
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
                        u.FirstName = rdr["FirstName"].ToString();
                        u.LastName = rdr["LastName"].ToString();
                        u.PermissionLevel = (byte)rdr["PermissionLevel"];
                        u.CreatedAt = (DateTime)rdr["CreatedAt"];
                    }
                }
            }

            return u;
        }

        /// <summary>
        /// Actualiza Username, Email, Password, FirstName y LastName del usuario.
        /// </summary>
        public static bool UpdateUsuario(int userId, string username, string email, string password, string firstName, string lastName)
        {
            const string sql = @"
                UPDATE Users
                   SET Username    = @user,
                       Email       = @mail,
                       Password    = @pwd,
                       FirstName   = @fname,
                       LastName    = @lname
                 WHERE UserId     = @id";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@fname", firstName);
                cmd.Parameters.AddWithValue("@lname", lastName);
                cmd.Parameters.AddWithValue("@id", userId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
