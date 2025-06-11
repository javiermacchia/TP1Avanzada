using System;
using System.Web.UI;
using BIZ;

namespace TP1Avanzada
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblMessage.Visible = false;
        }

        protected void BtRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            bool ok = DatosRegistro.RegisterUsuario(
                txtUsername.Text.Trim(),
                txtPassword.Text,
                txtEmail.Text.Trim(),
                txtFirstName.Text.Trim(),
                txtLastName.Text.Trim()
            );

            lblMessage.Visible = true;
            if (ok)
            {
                lblMessage.Text = "¡Registro exitoso! Ya puede iniciar sesión.";
                lblMessage.CssClass = "text-success";
                txtUsername.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
            }
            else
            {
                lblMessage.Text = "Error al registrar. El usuario o email ya existe.";
                lblMessage.CssClass = "text-danger";
            }
        }
    }
}
