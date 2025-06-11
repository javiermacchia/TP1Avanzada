using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace TP1Avanzada
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(
                new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddDays(-1) }
            );
            Response.Redirect("~/Login.aspx");
        }
    }
}
