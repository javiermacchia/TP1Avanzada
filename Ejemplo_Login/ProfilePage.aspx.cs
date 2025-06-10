using System;
using System.Drawing.Printing;
using System.Web.UI;

namespace Ejemplo_Login
{
    public partial class ProfilePage : Page
    {
        private int _userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[ProfilePage] Session UserId={(Session["UserId"] ?? "null")}");

            if (Session["UserId"] == null)
                Response.Redirect("~/Login.aspx");

            _userId = (int)Session["UserId"];

            if (!IsPostBack)
            {
                var u = BIZ.DatosLogin.GetUsuario(_userId);
                txtUsername.Text = u.Username;
                txtEmail.Text = u.Email;
                // dejamos txtPassword vacío
            }
            lblMsg.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var newUser = txtUsername.Text.Trim();
            var newEmail = txtEmail.Text.Trim();
            var newPassword = txtPassword.Text;

            // Si no ingresó nueva contraseña, mantenemos la actual:
            if (string.IsNullOrEmpty(newPassword))
            {
                var u = BIZ.DatosLogin.GetUsuario(_userId);
                newPassword = u.Password;
            }

            var ok = BIZ.DatosLogin.UpdateUsuario(
                _userId, newUser, newEmail, newPassword
            );

            lblMsg.Visible = true;
            if (ok)
            {
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Perfil actualizado con éxito.";
                // Refresca Session["UserName"]
                Session["UserName"] = newUser;
            }
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error al guardar cambios.";
            }
        }
    }
}
