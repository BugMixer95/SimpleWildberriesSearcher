namespace SimpleWildberriesSearcher.Core.Services.ExportService
{
    internal class ExportResult : IExportResult
    {
        public ExportStatusCode StatusCode { get; internal set; }

        public string? Nuance { get; internal set; }

        public Exception? Exception { get; internal set; }
    }
}
