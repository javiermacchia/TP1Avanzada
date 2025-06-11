using System;
using System.Web.Security;
using System.Web.UI;

namespace TP1Avanzada
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lt_mensaje.Visible = false;
        }

        protected void BtLogin_Click(object sender, EventArgs e)
        {
            var usuario = BIZ.DatosLogin.LoginUsuarioII(
                Txusuario.Text.Trim(),
                TxPassword.Text.Trim()
            );

            if (usuario.UserId > 0)
            {
                FormsAuthentication.SetAuthCookie(usuario.Username, false);
                Session["UserId"] = usuario.UserId;
                Session["UserName"] = usuario.Username;
                Session["PermissionLevel"] = usuario.PermissionLevel;
                if (usuario.PermissionLevel == 0)
                    Response.Redirect("~/AdminPage.aspx");
                else
                    Response.Redirect("~/Default.aspx");
            }
            else
            {
                lt_mensaje.Text = $"Usuario o contraseña inválidos.";
                lt_mensaje.CssClass = "text-danger";
                lt_mensaje.Visible = true;
                Txusuario.Text = "";
                TxPassword.Text = "";
                Txusuario.Focus();
            }
        }
    }
}
