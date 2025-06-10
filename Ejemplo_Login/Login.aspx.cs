using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Ejemplo_Login
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
            // Llama al servicio BIZ (contraseña en texto plano)
            var usuario = BIZ.DatosLogin.LoginUsuarioII(
                Txusuario.Text.Trim(),
                TxPassword.Text.Trim()
            );

            if (usuario.UserId > 0)
            {
                // 1) Autenticación
                FormsAuthentication.SetAuthCookie(usuario.Username, false);

                // 2) Guardar datos en Session
                Session["UserId"] = usuario.UserId;
                Session["UserName"] = usuario.Username;
                Session["PermissionLevel"] = usuario.PermissionLevel;
                System.Diagnostics.Debug.WriteLine($"[Login] Saved Session UserId={Session["UserId"]}");


                // 3) Redirigir según permiso
                if (usuario.PermissionLevel == 0)
                    Response.Redirect("~/AdminPage.aspx");
                else
                    Response.Redirect("~/Default.aspx");
            }
            else
            {
                // Credenciales inválidas
                lt_mensaje.Text = $"Usuario o contraseña inválidos.";
                lt_mensaje.CssClass = "text-danger";
                lt_mensaje.Visible = true;

                // Limpiar inputs y poner foco
                Txusuario.Text = "";
                TxPassword.Text = "";
                Txusuario.Focus();
            }
        }
    }
}
