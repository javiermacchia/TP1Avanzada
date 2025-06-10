using System;
using System.Web.UI;

namespace TP1Avanzada
{
    public partial class ProfilePage : Page
    {
        private int _userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Depuración
            System.Diagnostics.Debug.WriteLine($"[ProfilePage] Session UserId={(Session["UserId"] ?? "null")}");

            if (Session["UserId"] == null)
                Response.Redirect("~/Login.aspx");

            _userId = (int)Session["UserId"];

            if (!IsPostBack)
            {
                // Carga datos del usuario, incluyendo nombre y apellido
                var u = BIZ.DatosLogin.GetUsuario(_userId);
                txtFirstName.Text = u.FirstName;
                txtLastName.Text = u.LastName;
                txtUsername.Text = u.Username;
                txtEmail.Text = u.Email;
                // txtPassword y txtConfirm quedan vacíos
            }
            lblMsg.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var newUser = txtUsername.Text.Trim();
            var newEmail = txtEmail.Text.Trim();
            var newFirstName = txtFirstName.Text.Trim();
            var newLastName = txtLastName.Text.Trim();
            var newPassword = txtPassword.Text;

            // Si no ingresó nueva contraseña, mantenemos la actual:
            if (string.IsNullOrEmpty(newPassword))
            {
                var u = BIZ.DatosLogin.GetUsuario(_userId);
                newPassword = u.Password;
            }

            // Llamada actualizada al servicio con los nuevos campos
            var ok = BIZ.DatosLogin.UpdateUsuario(
                _userId,
                newUser,
                newEmail,
                newPassword,
                newFirstName,
                newLastName
            );

            lblMsg.Visible = true;
            if (ok)
            {
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Perfil actualizado con éxito.";
                // Refresca Session["UserName"] si cambió el username
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
