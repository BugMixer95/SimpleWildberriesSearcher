namespace SimpleWildberriesSearcher.Core.Services.ExportService
{
    public interface IExportResult
    {
        ExportStatusCode StatusCode { get; }
        string? Nuance { get; }
        Exception? Exception { get; }
    }
}
