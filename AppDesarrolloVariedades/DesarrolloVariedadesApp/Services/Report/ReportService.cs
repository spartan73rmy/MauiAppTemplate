using DesarrolloBVF.App.Domain.Entities;
using DesarrolloVariedadesApp.Services.Database;
using DesarrolloVariedadesApp.Services.Files;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Collections.ObjectModel;

namespace DesarrolloVariedadesApp.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IFilesServices filesServices;
        private readonly IDataBaseService dataBaseService;

        private Paragraph parrafo;
        private LineSeparator ls;
        private int cont;

        public ReportService(IFilesServices filesServices, IDataBaseService dataBaseService)
        {
            this.filesServices = filesServices;
            this.dataBaseService = dataBaseService;
        }

        public async Task DescargarReporteExcel(ObservableCollection<HistDesarrolloVariedadesApp> plantas, string name)
        {
            if(plantas.IsNullOrEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "No se encuentra informacion para realizar el documento.", "Entendido");
                return;
            }
            string fileName = $"Reporte_{name}.xlsx";
#if ANDROID
		    var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
#else
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage())
            {
                cont = 2;
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte Trazabilidad");
                worksheet.Rows.Height = 25;

                worksheet.Cells["A1"].Value = "ID Desarrollo";
                worksheet.Cells["B1"].Value = "Tipo";
                worksheet.Cells["C1"].Value = "Fecha Registro";
                worksheet.Cells["D1"].Value = "Consecutivo";
                worksheet.Cells["E1"].Value = "Cultivo";
                worksheet.Cells["F1"].Value = "Variedad";
                worksheet.Cells["G1"].Value = "Producto";
                worksheet.Cells["H1"].Value = "Presentacion";

                foreach (var item in plantas)
                {
                    worksheet.Cells[$"A{cont}"].Value = item.IdDesarrollo.ToString();
                    worksheet.Cells[$"B{cont}"].Value = item.TipoDesarrollo.Descripcion;
                    worksheet.Cells[$"C{cont}"].Value = item.FechaRegistro.ToString();
                    worksheet.Cells[$"D{cont}"].Value = item.Consecutivo.ToString();
                    worksheet.Cells[$"E{cont}"].Value = item.Producto.Variedad.Cultivo.Descripcion;
                    worksheet.Cells[$"F{cont}"].Value = item.Producto.Variedad.Descripcion;
                    worksheet.Cells[$"G{cont}"].Value = item.Producto.Descripcion;
                    worksheet.Cells[$"H{cont}"].Value = item.Presentacion.Descripcion;
                    cont += 1;
                }

                worksheet.Columns.AutoFit();
                package.SaveAs(new FileInfo(filePath));
                var mensaje = $"Se ha creado el reporte exitosamente en: {filePath}";
#if ANDROID
                var file = new ShareFile(filePath);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Enviar código QR",
                    File = file
                });
                await Task.WhenAll();
#endif
                await Application.Current.MainPage.DisplayAlert("Exito", mensaje, "Entendido");
            }
        }

        public async Task DescargarReporteIndividual(HistDesarrolloVariedadesApp plantaOrigen, string ubicacion = "")
        {
            string fileName = $"Reporte_{plantaOrigen.Lote}.pdf";

#if ANDROID
		    var filePath = Path.Combine(FileSystem.CacheDirectory, fileName);
#else
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                parrafo = new Paragraph($"Reporte individual de {plantaOrigen.Lote}\n")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(14);
                document.Add(parrafo);

                ls = new LineSeparator(new SolidLine());
                document.Add(ls);

                parrafo = new Paragraph($"\nDatos Generales" +
                    $"\n- Cultivo: {plantaOrigen.Producto.Variedad.Cultivo.Descripcion}." +
                    $"\n- Variedad: {plantaOrigen.Producto.Variedad.Descripcion}." +
                    $"\n- Producto: {plantaOrigen.Producto.Descripcion}." +
                    $"\n- Presentacion: {plantaOrigen.Presentacion.Descripcion}.\n\n")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
                    .SetFontSize(12);
                document.Add(parrafo);

                if (!string.IsNullOrEmpty(ubicacion))
                {
                    parrafo = new Paragraph($"Datos de Ubicación:\n{ubicacion}.\n")
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
                            .SetFontSize(12);
                    document.Add(parrafo);
                }

                var db = dataBaseService.Get();
                var imagenes = db.DesVarImagenes.Where(x => x.IdDesarrollo == plantaOrigen.IdDesarrollo && x.TipoAccion != "D") .ToList();
                if (imagenes.Count() > 0)
                {
                    parrafo = new Paragraph($"\nImagenes:")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
                        .SetFontSize(12);
                    document.Add(parrafo);

                    foreach (var imagen in imagenes)
                    {
                        var imgStream = await ConvertImageSourceToStreamAsync(filesServices.GetUrlImage(imagen.FullPath));
                        iText.Layout.Element.Image image = new iText.Layout.Element.Image(ImageDataFactory
                            .Create(imgStream))
                            .Scale(1,1)
                            .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);
                        document.Add(image);
                    }
                }

                document.Close();
                var mensaje = $"Se ha creado el reporte exitosamente en: {filePath}";
#if ANDROID
                var file = new ShareFile(filePath);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Enviar reporte",
                    File = file
                });
                await Task.WhenAll();
                mensaje = "Archivo Compartido exitosamente";
                File.Delete(filePath);
#endif
                await Application.Current.MainPage.DisplayAlert("Exito", mensaje, "Entendido");

            }
        }

        private async Task<byte[]> ConvertImageSourceToStreamAsync(string imageName)
        {
            byte[] byteArray;
            using (FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    byteArray = binaryReader.ReadBytes((int)fileStream.Length);
                }
            }
            return byteArray;
        }
    }
}