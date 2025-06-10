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

        /// <summary>
        /// Intenta registrar un usuario con nombre y apellido.
        /// Devuelve false si el username o email ya existen.
        /// </summary>
        public static bool RegisterUsuario(
            string username,
            string password,
            string email,
            string firstName,
            string lastName,
            byte permissionLevel = 1)
        {
            // 1) Comprueba duplicados
            const string checkSql = @"
                SELECT COUNT(*) 
                  FROM Users
                 WHERE Username = @user
                    OR Email    = @mail;";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(checkSql, conn))
            {
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@mail", email);
                conn.Open();
                int exists = Convert.ToInt32(cmd.ExecuteScalar());
                if (exists > 0)
                    return false;   // ya hay user o email
            }

            // 2) Inserta nuevo usuario
            const string insertSql = @"
                INSERT INTO Users
                    (Username, Password, Email, FirstName, LastName,
                     PermissionLevel, CreatedAt)
                VALUES
                    (@user, @pwd, @mail, @fname, @lname,
                     @perm, GETDATE());";

            using (var conn = new SqlConnection(GetConnectionString()))
            using (var cmd = new SqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.Parameters.AddWithValue("@mail", email);
                cmd.Parameters.AddWithValue("@fname", firstName);
                cmd.Parameters.AddWithValue("@lname", lastName);
                cmd.Parameters.AddWithValue("@perm", permissionLevel);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
