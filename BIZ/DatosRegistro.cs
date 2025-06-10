using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BIZ
{
    public class DatosRegistro
    {
        private static string GetConnectionString() =>
            ConfigurationManager
                .ConnectionStrings["SQLCLASE"]
                .ConnectionString;

        public static bool RegisterUsuario(string username, string password, string email, byte permissionLevel = 1)
        {
            string sql = $@"
                INSERT INTO Users
                    (Username, Password, Email, PermissionLevel, CreatedAt)
                VALUES
                    ('{username}', '{password}', '{email}', {permissionLevel}, '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}')";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
