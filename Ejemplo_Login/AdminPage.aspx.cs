using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Ejemplo_Login
{
    public partial class AdminPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Solo admins (PermissionLevel == 0)
            if (Session["UserId"] == null
                || Session["PermissionLevel"] == null
                || (byte)Session["PermissionLevel"] != 0)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            if (!IsPostBack)
                LoadUsers();
        }

        private void LoadUsers()
        {
            // Leer cadena desde Web.config
            string cs = System.Configuration.ConfigurationManager
                            .ConnectionStrings["SQLCLASE"]
                            .ConnectionString;

            const string sql = @"
                SELECT UserId, Username, Email, PermissionLevel, CreatedAt
                  FROM Users
                 ORDER BY CreatedAt DESC";

            // Patrón compatible con C# 7.3
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var dt = new DataTable();
                        dt.Load(rdr);
                        gvUsers.DataSource = dt;
                        gvUsers.DataBind();
                    }
                }
            }
        }
    }
}
