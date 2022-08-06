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
        public async Task<IExportResult> ExportAsync(string filePath, IEnumerable<ICardCollection> collections)
        {
            ExportResult result = new() { StatusCode = ExportStatusCode.Done };

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (File.Exists(filePath))
                    File.Delete(filePath);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new(fileInfo))
                {
                    foreach (var collection in collections)
                    {
                        if (collection.Name == string.Empty)
                            continue;

                        if (collection.Cards.Count == 0)
                        {
                            if (string.IsNullOrEmpty(result.Nuance))
                            {
                                result.Nuance = "Some of categories were exported with empty result because no data has been found. \n" +
                                    "Categories with empty result: ";
                            }
                            else
                            {
                                result.Nuance += "; ";
                            }

                            result.Nuance += collection.Name;
                            result.StatusCode = ExportStatusCode.DoneWithNuances;
                        }

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(collection.Name);
                        worksheet.Cells.LoadFromCollection(collection.Cards, PrintHeaders: true);
                    }

                    if (collections.Count() <= 0)
                    {
                        ExcelWorksheet dummyWorksheet = package.Workbook.Worksheets.Add("_");

                        result.StatusCode = ExportStatusCode.DoneWithNuances;
                        result.Nuance = "File is created, but there was no data to export. Choose another file.";
                    }

                    await package.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = ExportStatusCode.Failed;
                result.Exception = ex;
            }

            return result;
        }
    }
}
