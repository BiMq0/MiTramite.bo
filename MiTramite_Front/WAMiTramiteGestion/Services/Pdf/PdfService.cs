using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using MiTramite_Shared.DTOs.Reportes;

namespace MiTramite_Front.WAMiTramiteGestion.Services.Pdf
{
    public class PdfService : IPdfService
    {
        public PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GenerateReport(ReporteDashboardDTO data, int year)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Arial"));

                    page.Header().Element(compose => ComposeHeader(compose, year));
                    page.Content().Element(compose => ComposeContent(compose, data));
                    page.Footer().Element(ComposeFooter);
                });
            });

            return document.GeneratePdf();
        }

        void ComposeHeader(IContainer container, int year)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor("#003366");

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Reporte de Gestión {year}").Style(titleStyle);
                    column.Item().Text(text =>
                    {
                        text.Span("Generado el: ").SemiBold();
                        text.Span($"{DateTime.Now:dd/MM/yyyy HH:mm}");
                    });
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        void ComposeContent(IContainer container, ReporteDashboardDTO data)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(25);

                // Sección 1: Resumen de Trámites
                column.Item().Element(c => ComposeSectionTitle(c, "1. Resumen de Trámites por Mes"));
                column.Item().Element(c => ComposeTramitesTable(c, data.TramitesPorMes));

                // Sección 2: Rentistas
                column.Item().Element(c => ComposeSectionTitle(c, "2. Demografía de Rentistas"));
                column.Item().Element(c => ComposeRentistasTable(c, data.RentistasPorEdad));

                // Sección 3: Estados
                column.Item().Element(c => ComposeSectionTitle(c, "3. Estado de los Trámites"));
                column.Item().Element(c => ComposeEstadosTable(c, data.EstadosTramites));

                // Sección 4: Incumplimientos
                column.Item().Element(c => ComposeSectionTitle(c, "4. Reporte de Incumplimientos (Top 5)"));
                column.Item().Element(c => ComposeIncumplimientosTable(c, data.IncumplimientosTop));
            });
        }

        void ComposeSectionTitle(IContainer container, string title)
        {
            container.BorderBottom(1).BorderColor("#0056b3").PaddingBottom(5).Text(title).FontSize(16).SemiBold().FontColor("#0056b3");
        }

        void ComposeTramitesTable(IContainer container, List<ReporteTramitesPorMesDTO> data)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Mes");
                    header.Cell().Element(CellStyle).Text("Cantidad de Trámites");
                });

                foreach (var item in data)
                {
                    table.Cell().Element(CellStyle).Text(item.Mes);
                    table.Cell().Element(CellStyle).Text(item.Cantidad.ToString());
                }
            });
        }

        void ComposeRentistasTable(IContainer container, List<ReporteRentistasPorEdadDTO> data)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Rango de Edad");
                    header.Cell().Element(CellStyle).Text("Cantidad");
                });

                foreach (var item in data)
                {
                    table.Cell().Element(CellStyle).Text(item.RangoEdad);
                    table.Cell().Element(CellStyle).Text(item.Cantidad.ToString());
                }
            });
        }

        void ComposeEstadosTable(IContainer container, List<ReporteEstadoTramitesDTO> data)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Estado");
                    header.Cell().Element(CellStyle).Text("Cantidad");
                });

                foreach (var item in data)
                {
                    table.Cell().Element(CellStyle).Text(item.Estado);
                    table.Cell().Element(CellStyle).Text(item.Cantidad.ToString());
                }
            });
        }

        void ComposeIncumplimientosTable(IContainer container, List<ReporteIncumplimientosFuncionarioDTO> data)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Funcionario");
                    header.Cell().Element(CellStyle).Text("Correo");
                    header.Cell().Element(CellStyle).Text("Incumplimientos");
                });

                foreach (var item in data)
                {
                    table.Cell().Element(CellStyle).Text(item.NombreFuncionario);
                    table.Cell().Element(CellStyle).Text(item.CorreoFuncionario);
                    table.Cell().Element(CellStyle).Text(item.Cantidad.ToString());
                }
            });
        }

        IContainer CellStyle(IContainer container)
        {
            return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
        }

        void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(x =>
            {
                x.Span("Página ");
                x.CurrentPageNumber();
            });
        }
    }
}
