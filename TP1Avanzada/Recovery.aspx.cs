using System;
using System.Net.Mail;
using System.Web.UI;

namespace TP1Avanzada
{
    public partial class Recovery : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                lblMsg.Visible = false;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var token = BIZ.PasswordResetService.CreateToken(txtEmail.Text.Trim());
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                var resetUrl = $"{baseUrl}{ResolveUrl($"~/ResetPassword.aspx?token={token}")}";
                SendRecoveryEmail(txtEmail.Text.Trim(), resetUrl);
                lblMsg.CssClass = "text-success";
                lblMsg.Text = "Si existe ese email, recibirás en unos minutos un enlace para restablecer tu contraseña.";
            }
            catch (Exception ex)
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Error al enviar el email: " + ex.Message;
            }

            lblMsg.Visible = true;
        }

        private void SendRecoveryEmail(string toEmail, string resetUrl)
        {
            using (var msg = new MailMessage())
            {
                msg.To.Add(toEmail);
                msg.From = new MailAddress("javimacchi.jm@gmail.com");
                msg.Subject = "Recuperación de contraseña";
                msg.IsBodyHtml = true;
                msg.Body = $@"
                    <p>Has solicitado restablecer tu contraseña.</p>
                    <p>Haz clic en el siguiente enlace (válido 20 minutos):</p>
                    <p><a href=""{resetUrl}"">Restablecer contraseña</a></p>";

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(msg);
                }
            }
        }
    }
}
