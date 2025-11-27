using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MiTramite_Domain.Entities;
using MiTramite_Shared.DTOs.SolicitudTramiteDTOs;

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
                            <h2>Notificación de Trámite Completado con Exito</h2>
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

        public async Task EnviarCorreoNotificacionFuncionarioInfractor(string correoFuncionarioInfractor, SolicitudTramite incumplimiento)
        {
            try
            {
                Console.WriteLine($"[EMAIL SERVICE] Preparando notificación de incumplimiento para funcionario...");
                Console.WriteLine($"[EMAIL SERVICE] Funcionario: {correoFuncionarioInfractor}");

                // ============================================
                // DATOS QUE NECESITAS COMPLETAR AQUÍ:
                // ============================================
                // 1. ID del trámite → incumplimiento.???
                // 2. Fecha actual del incumplimiento → DateTime.Now
                // 3. Estado del trámite → incumplimiento.??? (ejemplo: "En Proceso", "Pendiente", etc.)
                // 4. Tipo de trámite → incumplimiento.??? (ejemplo: "Registro Civil", "Licencia de Conducir", etc.)
                // 5. Nombre completo del rentista → incumplimiento.??? (ejemplo: "Juan Pérez López")
                // ============================================

                var idTramite = incumplimiento.IdSolicitudTramite; // COMPLETA con la propiedad correcta
                var estadoTramite = "Estado del Trámite"; // COMPLETA: incumplimiento.EstadoTramite?.Nombre o similar
                var tipoTramite = "Tipo de Trámite"; // COMPLETA: incumplimiento.TipoTramite?.Nombre o similar
                var nombreRentista = "Nombre del Rentista"; // COMPLETA: incumplimiento.Rentista?.NombreCompleto o similar

                string asunto = "⚠️ Notificación de Incumplimiento Registrado - Acción Requerida";
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
                                    max-width: 650px;
                                    margin: 0 auto;
                                    background-color: #ffffff;
                                    border-radius: 10px;
                                    box-shadow: 0 2px 15px rgba(0, 0, 0, 0.15);
                                    overflow: hidden;
                                    border-top: 5px solid #dc2626;
                                }}
                                .header {{
                                    background: linear-gradient(135deg, #7f1d1d 0%, #dc2626 100%);
                                    color: white;
                                    padding: 40px 20px;
                                    text-align: center;
                                }}
                                .header h1 {{
                                    margin: 0;
                                    font-size: 28px;
                                    font-weight: bold;
                                }}
                                .header .icon {{
                                    font-size: 48px;
                                    margin-bottom: 10px;
                                }}
                                .content {{
                                    padding: 40px 30px;
                                    color: #333333;
                                }}
                                .alert-box {{
                                    background-color: #fef2f2;
                                    border-left: 4px solid #dc2626;
                                    padding: 20px;
                                    margin: 25px 0;
                                    border-radius: 4px;
                                }}
                                .alert-box h3 {{
                                    color: #991b1b;
                                    margin-top: 0;
                                    font-size: 18px;
                                }}
                                .info-section {{
                                    background-color: #f8fafc;
                                    padding: 20px;
                                    border-radius: 8px;
                                    margin: 20px 0;
                                    border: 1px solid #e2e8f0;
                                }}
                                .info-row {{
                                    display: flex;
                                    padding: 10px 0;
                                    border-bottom: 1px solid #e2e8f0;
                                }}
                                .info-row:last-child {{
                                    border-bottom: none;
                                }}
                                .info-label {{
                                    font-weight: bold;
                                    color: #1e3a8a;
                                    min-width: 180px;
                                }}
                                .info-value {{
                                    color: #334155;
                                }}
                                .warning-section {{
                                    background: linear-gradient(135deg, #fef3c7 0%, #fde68a 100%);
                                    padding: 20px;
                                    border-radius: 8px;
                                    margin: 25px 0;
                                    border-left: 4px solid #f59e0b;
                                }}
                                .warning-section h3 {{
                                    color: #92400e;
                                    margin-top: 0;
                                }}
                                .consequences-list {{
                                    margin: 15px 0;
                                    padding-left: 20px;
                                }}
                                .consequences-list li {{
                                    margin: 8px 0;
                                    color: #78350f;
                                }}
                                .footer {{
                                    background-color: #1e293b;
                                    color: #e2e8f0;
                                    padding: 25px;
                                    text-align: center;
                                    font-size: 14px;
                                }}
                                .footer strong {{
                                    color: #ffffff;
                                }}
                                .divider {{
                                    border-top: 2px solid #e2e8f0;
                                    margin: 25px 0;
                                }}
                                .emphasis {{
                                    color: #dc2626;
                                    font-weight: bold;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""icon"">⚠️</div>
                                    <h1>Notificación de Incumplimiento</h1>
                                    <p>Se ha registrado un incumplimiento en el sistema</p>
                                </div>
                                
                                <div class=""content"">
                                    <div class=""alert-box"">
                                        <h3>⚠️ ATENCIÓN: Incumplimiento Registrado</h3>
                                        <p>Estimado/a funcionario/a,</p>
                                        <p>Le informamos que se ha registrado un <span class=""emphasis"">incumplimiento</span> relacionado con el trámite que estaba bajo su responsabilidad.</p>
                                    </div>
                                    
                                    <h3 style=""color: #1e3a8a; margin-top: 30px;"">📋 Detalles del Incumplimiento</h3>
                                    <div class=""info-section"">
                                        <div class=""info-row"">
                                            <span class=""info-label"">ID de Trámite:</span>
                                            <span class=""info-value"">#{idTramite}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Fecha de Registro:</span>
                                            <span class=""info-value"">{DateTime.Now:dd/MM/yyyy HH:mm}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Estado del Trámite:</span>
                                            <span class=""info-value"">{estadoTramite}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Tipo de Trámite:</span>
                                            <span class=""info-value"">{tipoTramite}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Rentista Afectado:</span>
                                            <span class=""info-value"">{nombreRentista}</span>
                                        </div>
                                    </div>
                                    
                                    <div class=""warning-section"">
                                        <h3>⚖️ Consecuencias y Acciones</h3>
                                        <p>De acuerdo con las políticas de la institución, se procederá con las siguientes acciones:</p>
                                        <ul class=""consequences-list"">
                                            <li>Registro formal del incumplimiento en su historial laboral</li>
                                            <li>Evaluación del caso por parte del departamento de recursos humanos</li>
                                            <li>Posible aplicación de sanciones administrativas según la gravedad</li>
                                            <li>Reasignación del trámite a otro funcionario disponible</li>
                                            <li>Seguimiento especial de sus futuros trámites asignados</li>
                                        </ul>
                                    </div>
                                    
                                    <div class=""divider""></div>
                                    
                                    <div style=""background-color: #eff6ff; padding: 20px; border-radius: 8px; border-left: 4px solid #1e3a8a;"">
                                        <h3 style=""color: #1e3a8a; margin-top: 0;"">📞 Próximos Pasos</h3>
                                        <p style=""margin: 10px 0; color: #1e40af;"">
                                            Se le convocará a una reunión con su supervisor para discutir los detalles del incumplimiento. 
                                            Por favor, prepárese para proporcionar cualquier explicación o documentación relevante.
                                        </p>
                                        <p style=""margin: 10px 0; color: #1e40af;"">
                                            <strong>Es importante que revise sus procesos de trabajo y gestión de tiempo para evitar futuros incumplimientos.</strong>
                                        </p>
                                    </div>
                                    
                                    <div class=""divider""></div>
                                    
                                    <p style=""color: #64748b; font-size: 14px; text-align: center;"">
                                        Este es un correo automático del sistema MiTrámite.bo. 
                                        Para cualquier consulta, por favor contacte a su supervisor inmediato.
                                    </p>
                                </div>
                                
                                <div class=""footer"">
                                    <p><strong>MiTrámite.bo</strong> - Sistema de Gestión de Trámites</p>
                                    <p>© 2025 - Todos los derechos reservados</p>
                                    <p style=""margin-top: 15px; font-size: 12px; opacity: 0.8;"">
                                        Este correo fue enviado a {correoFuncionarioInfractor} como parte del proceso de notificación de incumplimientos.
                                    </p>
                                </div>
                            </div>
                        </body>
                    </html>";

                await EnviarCorreoAsync(correoFuncionarioInfractor, asunto, cuerpo);
                Console.WriteLine($"[EMAIL SERVICE] Notificación de incumplimiento enviada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL SERVICE] ERROR al enviar notificación de incumplimiento: {ex.Message}");
                throw new InvalidOperationException("Error al enviar notificación de incumplimiento", ex);
            }
        }

        public async Task EnviarCorreoReasignacionFuncionarioNuevo(string correoFuncionarioNuevo, SolicitudTramite tramite)
        {
            try
            {
                Console.WriteLine($"[EMAIL SERVICE] Preparando notificación de reasignación de trámite...");
                Console.WriteLine($"[EMAIL SERVICE] Nuevo funcionario: {correoFuncionarioNuevo}");
                var datosTramite = new SolicitudTramiteRegistroDTO(tramite);
                var idTramite = datosTramite.IdSolicitudTramite;
                var tipoTramite = datosTramite.NombreTipoTramite ?? "No disponible";
                var estadoActual = datosTramite.Estado ?? "No disponible";
                var nombreRentista = datosTramite.NombreCompletoRentista ?? "No disponible";
                var correoRentista = datosTramite.CorreoRentista ?? "No disponible";
                var fechaCreacion = datosTramite.FechaSolicitud.ToString("dd/MM/yyyy HH:mm");

                string asunto = "Nuevo Trámite Asignado por Reasignación - Acción Requerida";
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
                                    max-width: 650px;
                                    margin: 0 auto;
                                    background-color: #ffffff;
                                    border-radius: 10px;
                                    box-shadow: 0 2px 15px rgba(0, 0, 0, 0.15);
                                    overflow: hidden;
                                    border-top: 5px solid #2563eb;
                                }}
                                .header {{
                                    background: linear-gradient(135deg, #1e3a8a 0%, #2563eb 100%);
                                    color: white;
                                    padding: 40px 20px;
                                    text-align: center;
                                }}
                                .header h1 {{
                                    margin: 0;
                                    font-size: 28px;
                                    font-weight: bold;
                                }}
                                .header .icon {{
                                    font-size: 48px;
                                    margin-bottom: 10px;
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
                                .notification-box {{
                                    background: linear-gradient(135deg, #dbeafe 0%, #bfdbfe 100%);
                                    border-left: 4px solid #2563eb;
                                    padding: 25px;
                                    margin: 25px 0;
                                    border-radius: 8px;
                                }}
                                .notification-box h3 {{
                                    color: #1e3a8a;
                                    margin-top: 0;
                                    font-size: 20px;
                                }}
                                .notification-box p {{
                                    color: #1e40af;
                                    margin: 10px 0;
                                    line-height: 1.6;
                                }}
                                .info-section {{
                                    background-color: #f8fafc;
                                    padding: 25px;
                                    border-radius: 8px;
                                    margin: 25px 0;
                                    border: 1px solid #e2e8f0;
                                }}
                                .info-section h3 {{
                                    color: #1e3a8a;
                                    margin-top: 0;
                                    margin-bottom: 20px;
                                }}
                                .info-row {{
                                    display: flex;
                                    padding: 12px 0;
                                    border-bottom: 1px solid #e2e8f0;
                                }}
                                .info-row:last-child {{
                                    border-bottom: none;
                                }}
                                .info-label {{
                                    font-weight: bold;
                                    color: #1e3a8a;
                                    min-width: 180px;
                                }}
                                .info-value {{
                                    color: #334155;
                                    flex: 1;
                                }}
                                .priority-badge {{
                                    display: inline-block;
                                    background: linear-gradient(135deg, #fbbf24 0%, #f59e0b 100%);
                                    color: #78350f;
                                    padding: 8px 16px;
                                    border-radius: 20px;
                                    font-weight: bold;
                                    font-size: 14px;
                                    text-transform: uppercase;
                                }}
                                .urgent-badge {{
                                    display: inline-block;
                                    background: linear-gradient(135deg, #f87171 0%, #ef4444 100%);
                                    color: white;
                                    padding: 8px 16px;
                                    border-radius: 20px;
                                    font-weight: bold;
                                    font-size: 14px;
                                    text-transform: uppercase;
                                }}
                                .action-section {{
                                    background-color: #eff6ff;
                                    padding: 25px;
                                    border-radius: 8px;
                                    margin: 25px 0;
                                    border-left: 4px solid #3b82f6;
                                }}
                                .action-section h3 {{
                                    color: #1e40af;
                                    margin-top: 0;
                                }}
                                .action-list {{
                                    margin: 15px 0;
                                    padding-left: 0;
                                    list-style: none;
                                }}
                                .action-list li {{
                                    padding: 12px 0 12px 35px;
                                    position: relative;
                                    color: #1e40af;
                                    line-height: 1.6;
                                }}
                                .action-list li:before {{
                                    content: '✓';
                                    position: absolute;
                                    left: 0;
                                    color: #3b82f6;
                                    font-weight: bold;
                                    font-size: 18px;
                                }}
                                .context-box {{
                                    background-color: #fef3c7;
                                    padding: 20px;
                                    border-radius: 8px;
                                    margin: 25px 0;
                                    border-left: 4px solid #f59e0b;
                                }}
                                .context-box h4 {{
                                    color: #92400e;
                                    margin-top: 0;
                                    font-size: 16px;
                                }}
                                .context-box p {{
                                    color: #78350f;
                                    margin: 8px 0;
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
                                    box-shadow: 0 4px 6px rgba(30, 58, 138, 0.3);
                                }}
                                .footer {{
                                    background-color: #1e293b;
                                    color: #e2e8f0;
                                    padding: 25px;
                                    text-align: center;
                                    font-size: 14px;
                                }}
                                .footer strong {{
                                    color: #ffffff;
                                }}
                                .divider {{
                                    border-top: 2px solid #e2e8f0;
                                    margin: 25px 0;
                                }}
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <div class=""header"">
                                    <div class=""icon"">🔄</div>
                                    <h1>Trámite Reasignado</h1>
                                    <p>Se te ha asignado un nuevo trámite que requiere atención prioritaria</p>
                                </div>
                                
                                <div class=""content"">
                                    <div class=""notification-box"">
                                        <h3>Nueva Asignación de Trámite</h3>
                                        <p>Estimado/a funcionario/a,</p>
                                        <p>Se te ha asignado un nuevo trámite que ha sido <strong>reasignado</strong> debido a un incumplimiento por parte de un compañero del equipo. Este trámite requiere tu atención inmediata para garantizar la continuidad del servicio.</p>
                                    </div>
                                    
                                    <div style=""text-align: center; margin: 25px 0;"">
                                        <span class=""urgent-badge"">REQUIERE ATENCIÓN INMEDIATA</span>
                                    </div>
                                    
                                    <div class=""info-section"">
                                        <h3>📋 Detalles del Trámite Reasignado</h3>
                                        <div class=""info-row"">
                                            <span class=""info-label"">ID de Trámite:</span>
                                            <span class=""info-value"">#{idTramite}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Tipo de Trámite:</span>
                                            <span class=""info-value"">{tipoTramite}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Estado Actual:</span>
                                            <span class=""info-value"">{estadoActual}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Rentista:</span>
                                            <span class=""info-value"">{nombreRentista}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Correo del Rentista:</span>
                                            <span class=""info-value"">{correoRentista}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Fecha de Creación:</span>
                                            <span class=""info-value"">{fechaCreacion:dd/MM/yyyy HH:mm}</span>
                                        </div>
                                        <div class=""info-row"">
                                            <span class=""info-label"">Fecha de Reasignación:</span>
                                            <span class=""info-value"">{DateTime.Now:dd/MM/yyyy HH:mm}</span>
                                        </div>
                                    </div>
                                    
                                    <div class=""context-box"">
                                        <h4>ℹ️ Contexto de la Reasignación</h4>
                                        <p>Este trámite fue previamente asignado a otro funcionario que incurrió en un incumplimiento de sus responsabilidades. Para garantizar la calidad del servicio y el cumplimiento de los plazos establecidos, se ha decidido reasignar este caso bajo tu gestión.</p>
                                        <p><strong>Tu profesionalismo y compromiso son fundamentales para resolver esta situación.</strong></p>
                                    </div>
                                    
                                    <div class=""action-section"">
                                        <h3>Acciones Requeridas</h3>
                                        <ul class=""action-list"">
                                            <li>Revisar inmediatamente los detalles completos del trámite en el sistema</li>
                                            <li>Verificar toda la documentación presentada por el rentista</li>
                                            <li>Contactar al rentista para informarle sobre la reasignación si es necesario</li>
                                            <li>Evaluar el estado actual y determinar los siguientes pasos</li>
                                            <li>Actualizar el estado del trámite conforme avances en su procesamiento</li>
                                            <li>Cumplir con los plazos establecidos para evitar demoras adicionales</li>
                                        </ul>
                                    </div>
                                    
                                    <div class=""divider""></div>
                                    
                                    <div style=""text-align: center;"">
                                        <p style=""color: #1e3a8a; font-size: 16px; margin-bottom: 15px;"">
                                            <strong>Accede al sistema para comenzar a trabajar en este trámite:</strong>
                                        </p>
                                        <a href=""http://localhost:5080"" class=""cta-button"">Ir al Sistema MiTrámite.bo</a>
                                    </div>
                                    
                                    <div class=""divider""></div>
                                    
                                    <div style=""background-color: #f1f5f9; padding: 20px; border-radius: 8px; text-align: center;"">
                                        <p style=""color: #475569; margin: 5px 0;"">
                                            <strong>¿Necesitas ayuda?</strong>
                                        </p>
                                        <p style=""color: #64748b; font-size: 14px; margin: 5px 0;"">
                                            Si tienes preguntas o necesitas asistencia con este trámite, 
                                            no dudes en contactar a tu supervisor o al equipo de soporte técnico.
                                        </p>
                                    </div>
                                </div>
                                
                                <div class=""footer"">
                                    <p><strong>MiTrámite.bo</strong> - Sistema de Gestión de Trámites</p>
                                    <p>© 2025 - Todos los derechos reservados</p>
                                    <p style=""margin-top: 15px; font-size: 12px; opacity: 0.8;"">
                                        Este correo fue enviado a {correoFuncionarioNuevo} como parte del proceso de reasignación de trámites.
                                    </p>
                                </div>
                            </div>
                        </body>
                    </html>";

                await EnviarCorreoAsync(correoFuncionarioNuevo, asunto, cuerpo);
                Console.WriteLine($"[EMAIL SERVICE] Notificación de reasignación enviada exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EMAIL SERVICE] ERROR al enviar notificación de reasignación: {ex.Message}");
                throw new InvalidOperationException("Error al enviar notificación de reasignación", ex);
            }
        }
    }


}
