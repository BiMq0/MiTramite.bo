using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MiTramite_Back.Logica_De_Negocio.Services.EmailSvc
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> EnviarCorreoAsync(string destinatario, string asunto, string cuerpo, string? correoEmisor = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var smtpServer = _configuration["Email:SmtpServer"] ?? throw new InvalidOperationException("SMTP Server no configurado");
                var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
                var emailEmisor = _configuration["Email:Remitente"] ?? throw new InvalidOperationException("Email remitente no configurado");
                var passwordEmisor = _configuration["Email:Password"] ?? throw new InvalidOperationException("Password de email no configurado");
                var enableSsl = bool.Parse(_configuration["Email:EnableSSL"] ?? "true");

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(emailEmisor, passwordEmisor);

                    using (var mailMessage = new MailMessage(correoEmisor ?? emailEmisor, destinatario))
                    {
                        mailMessage.Subject = asunto;
                        mailMessage.Body = cuerpo;
                        mailMessage.IsBodyHtml = true;

                        await client.SendMailAsync(mailMessage);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al enviar correo: {ex.Message}", ex);
            }
        }

        public async Task<bool> NotificarCompletacionTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string correoFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                string asunto = $"Tu trámite '{nombreTramite}' ha sido completado";
                string cuerpo = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <h2>Notificación de Completación de Trámite</h2>
                            <p>Hola {nombreRentista},</p>
                            <p>Nos complace informarte que tu trámite <strong>{nombreTramite}</strong> ha sido <strong style='color: green;'>completado</strong> exitosamente.</p>
                            <p>Puedes consultar los detalles de tu trámite en nuestra plataforma.</p>
                            <hr>
                            <p><small>Este correo fue enviado por {correoFuncionario}</small></p>
                            <p><small>Sistema MiTrámite © 2025</small></p>
                        </body>
                    </html>";

                return await EnviarCorreoAsync(correoRentista, asunto, cuerpo, correoFuncionario, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al notificar completación de trámite", ex);
            }
        }

        public async Task<bool> NotificarRechazoTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string motivo, string correoFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                string asunto = $"Tu trámite '{nombreTramite}' ha sido rechazado";
                string cuerpo = $@"
                    <html>
                        <body style='font-family: Arial, sans-serif; color: #333;'>
                            <h2>Notificación de Rechazo de Trámite</h2>
                            <p>Hola {nombreRentista},</p>
                            <p>Lamentamos informarte que tu trámite <strong>{nombreTramite}</strong> ha sido <strong style='color: red;'>rechazado</strong>.</p>
                            <h3>Motivo del rechazo:</h3>
                            <p style='background-color: #f5f5f5; padding: 10px; border-left: 4px solid #ff6b6b;'>
                                {motivo}
                            </p>
                            <p>Por favor, revisa los detalles y contacta con nosotros si deseas presentar una apelación.</p>
                            <hr>
                            <p><small>Este correo fue enviado por {correoFuncionario}</small></p>
                            <p><small>Sistema MiTrámite © 2025</small></p>
                        </body>
                    </html>";

                return await EnviarCorreoAsync(correoRentista, asunto, cuerpo, correoFuncionario, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al notificar rechazo de trámite", ex);
            }
        }
    }
}
