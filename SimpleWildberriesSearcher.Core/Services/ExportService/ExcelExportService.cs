using OfficeOpenXml;
using SimpleWildberriesSearcher.Core.Models;

namespace SimpleWildberriesSearcher.Core.Services.ExportService
{
    /// <summary>
    /// Service which is used to export collections of cards to an Excel file.
    /// </summary>
    public class ExcelExportService : IExportService
    {
        /// <inheritdoc />
        /// <param name="filePath">Path of an Excel file where collections should be exported to.</param>
        public async Task ExportAsync(string filePath, IEnumerable<ICardCollection> items)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            if (File.Exists(filePath))
                File.Delete(filePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new(fileInfo))
            {
                foreach (var collection in items)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(collection.Name);
                    worksheet.Cells.LoadFromCollection(collection.Cards, PrintHeaders: true);
                }

                await package.SaveAsync();
            }
        }
    }
}
