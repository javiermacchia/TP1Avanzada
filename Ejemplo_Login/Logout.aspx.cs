using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Ejemplo_Login
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1) Quitar cookie de forms auth
            FormsAuthentication.SignOut();
            // 2) Limpiar sesión
            Session.Clear();
            Session.Abandon();
            // 3) Forzar nueva sesión (opcional)
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddDays(-1) }
            );
            // 4) Redirigir al login o al home
            Response.Redirect("~/Login.aspx");
        }
    }
}
