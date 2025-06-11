using System;
using System.Web.UI;

namespace TP1Avanzada
{
    public partial class ResetPassword : Page
    {
        private Guid _token;

        protected void Page_Load(object sender, EventArgs e)
        {
            litError.Visible = false;

            var t = Request.QueryString["token"];
            if (string.IsNullOrEmpty(t) || !Guid.TryParse(t, out _token))
            {
                ShowError("Token inválido.");
                return;
            }

            if (!BIZ.PasswordResetService.ValidateToken(_token))
            {
                ShowError("Token expirado o ya utilizado.");
                return;
            }

            pnlForm.Visible = true;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (BIZ.PasswordResetService.ResetPassword(_token, txtPassword.Text))
            {
                ShowMessage("Contraseña actualizada con éxito.", isError: false);
                pnlForm.Visible = false;
            }
            else
            {
                ShowError("No se pudo actualizar la contraseña.");
            }
        }

        private void ShowError(string msg)
        {
            litError.CssClass = "text-danger";
            litError.Text = msg;
            litError.Visible = true;
        }

        private void ShowMessage(string msg, bool isError = true)
        {
            litError.CssClass = isError ? "text-danger" : "text-success";
            litError.Text = msg;
            litError.Visible = true;
        }
    }
}
