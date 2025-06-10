using System;
using System.Web.UI;

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
            // Si fallaran los validadores, no procesamos
            if (!Page.IsValid) return;

            // Llamada al BIZ
            bool ok = BIZ.DatosRegistro
                           .RegisterUsuario(
                               txtUsername.Text.Trim(),
                               txtPassword.Text,
                               txtEmail.Text.Trim()
                            );

            lblMessage.Visible = true;
            if (ok)
            {
                lblMessage.Text = "¡Registro exitoso! Ya puede iniciar sesión.";
                lblMessage.CssClass = "text-success";
                // Opcional: limpiar campos
                txtUsername.Text = "";
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
            else
            {
                lblMessage.Text = "Error al registrar. El usuario o email puede existir.";
                lblMessage.CssClass = "text-danger";
            }
        }
    }
}
