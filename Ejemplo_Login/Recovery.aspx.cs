using System;
using System.Net.Mail;
using System.Web.UI;

namespace Ejemplo_Login
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
                // 1) Genera el token en la BD
                var token = BIZ.PasswordResetService.CreateToken(txtEmail.Text.Trim());

                // 2) Construye la URL de restablecimiento
                var baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                var resetUrl = $"{baseUrl}{ResolveUrl($"~/ResetPassword.aspx?token={token}")}";

                // 3) Envía el correo
                SendRecoveryEmail(txtEmail.Text.Trim(), resetUrl);

                // 4) Muestra mensaje de éxito
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
                    // El SmtpClient recoge host, puerto, credenciales y SSL
                    // desde la sección <system.net><mailSettings> de tu Web.config
                    smtp.Send(msg);
                }
            }
        }
    }
}
