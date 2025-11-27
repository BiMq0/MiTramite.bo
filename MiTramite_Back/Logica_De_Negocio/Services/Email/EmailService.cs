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
                Console.WriteLine($"[EMAIL SERVICE] Iniciando envío de correo...");
                Console.WriteLine($"[EMAIL SERVICE] Destinatario: {destinatario}");
                Console.WriteLine($"[EMAIL SERVICE] Asunto: {asunto}");

                var smtpServer = _configuration["Email:SmtpServer"] ?? throw new InvalidOperationException("SMTP Server no configurado");
                var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
                var emailEmisor = _configuration["Email:Remitente"] ?? throw new InvalidOperationException("Email remitente no configurado");
                var passwordEmisor = _configuration["Email:Password"] ?? throw new InvalidOperationException("Password de email no configurado");
                var enableSsl = bool.Parse(_configuration["Email:EnableSSL"] ?? "true");

                Console.WriteLine($"[EMAIL SERVICE] Servidor SMTP: {smtpServer}:{smtpPort}");
                Console.WriteLine($"[EMAIL SERVICE] Emisor: {emailEmisor}");

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.EnableSsl = enableSsl;
                    client.Credentials = new NetworkCredential(emailEmisor, passwordEmisor);

                    using (var mailMessage = new MailMessage(correoEmisor ?? emailEmisor, destinatario))
                    {
                        mailMessage.Subject = asunto;
                        mailMessage.Body = cuerpo;
                        mailMessage.IsBodyHtml = true;

                        Console.WriteLine($"[EMAIL SERVICE] Enviando mensaje vía SMTP...");
                        await client.SendMailAsync(mailMessage);
                        Console.WriteLine($"[EMAIL SERVICE] CORREO ENVIADO SATISFACTORIAMENTE a {destinatario}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL SERVICE] ERROR AL ENVIAR CORREO: {ex.Message}");
                Console.WriteLine($"[EMAIL SERVICE] Stack Trace: {ex.StackTrace}");
                throw new InvalidOperationException($"Error al enviar correo: {ex.Message}", ex);
            }
        }

        public async Task<bool> NotificarCompletacionTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string correoFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                Console.WriteLine($"[EMAIL SERVICE] Preparando notificación de completación de trámite...");
                Console.WriteLine($"[EMAIL SERVICE] Trámite: {nombreTramite} | Rentista: {nombreRentista} ({correoRentista})");

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
                Console.WriteLine($"[EMAIL SERVICE] ERROR en notificación de completación: {ex.Message}");
                throw new InvalidOperationException("Error al notificar completación de trámite", ex);
            }
        }

        public async Task<bool> NotificarRechazoTramiteAsync(string correoRentista, string nombreRentista, string nombreTramite, string motivo, string correoFuncionario, CancellationToken cancellationToken = default)
        {
            try
            {
                Console.WriteLine($"[EMAIL SERVICE] Preparando notificación de rechazo de trámite...");
                Console.WriteLine($"[EMAIL SERVICE] Trámite: {nombreTramite} | Rentista: {nombreRentista} ({correoRentista}) | Motivo: {motivo}");

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
                            <p>Por favor, revisa los detalles y vuelve a presentar tu solicitud de trámite.</p>
                            <hr>
                            <p><small>Este correo fue enviado por {correoFuncionario}</small></p>
                            <p><small>Sistema MiTrámite © 2025</small></p>
                        </body>
                    </html>";

                return await EnviarCorreoAsync(correoRentista, asunto, cuerpo, correoFuncionario, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL SERVICE] ERROR en notificación de rechazo: {ex.Message}");
                throw new InvalidOperationException("Error al notificar rechazo de trámite", ex);
            }
        }

        public async Task<bool> EnviarBienvenidaRentistaAsync(string correoRentista, string nombreRentista, CancellationToken cancellationToken = default)
        {
            try
            {
                Console.WriteLine($"[EMAIL SERVICE] Preparando correo de bienvenida...");
                Console.WriteLine($"[EMAIL SERVICE] Nuevo rentista: {nombreRentista} ({correoRentista})");

                string asunto = "¡Bienvenido a MiTrámite.bo!";
                string cuerpo = $@"
                    <html>
                        <head>
                            <style>
                                body {{
                                    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                                    background-color: #f0f4f8;
                                    margin: 0;
                                    padding: 20px;
                                }}
                                .container {{
                                    max-width: 600px;
                                    margin: 0 auto;
                                    background-color: #ffffff;
                                    border-radius: 10px;
                                    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                                    overflow: hidden;
                                }}
                                .header {{
                                    background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%);
                                    color: white;
                                    padding: 40px 20px;
                                    text-align: center;
                                }}
                                .header h1 {{
                                    margin: 0;
                                    font-size: 32px;
                                    font-weight: bold;
                                }}
                                .header p {{
                                    margin: 10px 0 0 0;
                                    font-size: 16px;
                                    opacity: 0.95;
                                }}
                                .content {{
                                    padding: 40px 30px;
                                    color: #333333;
                                }}
                                .content h2 {{
                                    color: #1e3a8a;
                                    margin-top: 0;
                                    font-size: 24px;
                                }}
                                .content p {{
                                    line-height: 1.8;
                                    font-size: 16px;
                                    margin: 15px 0;
                                }}
                                .features {{
                                    background-color: #f0f4f8;
                                    padding: 20px;
                                    border-radius: 8px;
                                    margin: 20px 0;
                                }}
                                .feature-item {{
                                    display: flex;
                                    align-items: center;
                                    margin: 12px 0;
                                    color: #334155;
                                }}
                                .feature-icon {{
                                    color: #3b82f6;
                                    font-weight: bold;
                                    margin-right: 12px;
                                    font-size: 18px;
                                }}
                                .cta-button {{
                                    display: inline-block;
                                    background: linear-gradient(135deg, #1e3a8a 0%, #3b82f6 100%);
                                    color: white;
                                    padding: 14px 30px;
                                    text-decoration: none;
                                    border-radius: 6px;
                                    margin: 20px 0;
                                    font-weight: bold;
                                    text-align: center;
                                }}
                                .footer {{
                                    background-color: #f0f4f8;
                                    padding: 20px;
                                    text-align: center;
                                    border-top: 1px solid #e2e8f0;
                                    font-size: 14px;
                                    color: #64748b;
                                }}
                                .divider {{
                                    border-top: 2px solid #e2e8f0;
                                    margin: 20px 0;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <h1>🎉 ¡Bienvenido!</h1>
                                    <p>Tu cuenta en MiTrámite.bo ha sido creada exitosamente</p>
                                </div>
                                <div class=""content"">
                                    <h2>Hola {nombreRentista},</h2>
                                    <p>Nos complace recibirte en <strong>MiTrámite.bo</strong>, la plataforma moderna para gestionar tus trámites de forma segura y eficiente.</p>
                                    
                                    <div class=""divider""></div>
                                    
                                    <h3 style=""color: #1e3a8a; font-size: 18px;"">¿Qué puedes hacer ahora?</h3>
                                    <div class=""features"">
                                        <div class=""feature-item"">
                                            <span class=""feature-icon"">✓</span>
                                            <span>Crear nuevas solicitudes de trámite de forma rápida</span>
                                        </div>
                                        <div class=""feature-item"">
                                            <span class=""feature-icon"">✓</span>
                                            <span>Subir documentos necesarios para tus trámites</span>
                                        </div>
                                        <div class=""feature-item"">
                                            <span class=""feature-icon"">✓</span>
                                            <span>Seguimiento en tiempo real del estado de tus solicitudes</span>
                                        </div>
                                        <div class=""feature-item"">
                                            <span class=""feature-icon"">✓</span>
                                            <span>Recibir notificaciones de actualizaciones importantes</span>
                                        </div>
                                    </div>
                                    
                                    <div class=""divider""></div>
                                    
                                    <p>Si tienes alguna pregunta o necesitas ayuda, no dudes en contactarnos. Estamos aquí para asistirte en cada paso del camino.</p>
                                    
                                    <div style=""text-align: center;"">
                                        <a href=""http://localhost:5080"" class=""cta-button"">Ir a MiTrámite.bo</a>
                                    </div>
                                </div>
                                <div class=""footer"">
                                    <p><strong>MiTrámite.bo</strong> © 2025 - Todos los derechos reservados</p>
                                    <p>Este correo fue enviado a {correoRentista} porque registraste una cuenta en nuestro sistema.</p>
                                </div>
                            </div>
                        </body>
                    </html>";

                return await EnviarCorreoAsync(correoRentista, asunto, cuerpo, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL SERVICE] ERROR en correo de bienvenida: {ex.Message}");
                throw new InvalidOperationException("Error al enviar correo de bienvenida", ex);
            }
        }
    }
}
