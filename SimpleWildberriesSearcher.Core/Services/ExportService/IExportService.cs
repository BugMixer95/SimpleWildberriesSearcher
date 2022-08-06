using SimpleWildberriesSearcher.Core.Models;namespace SimpleWildberriesSearcher.Core.Services.ExportService
{
    /// <summary>
    /// Provides a contract to export collections of cards to different sources.
    /// </summary>
    public interface IExportService
    {
        /// <summary>
        /// Asynchronously exports collections of cards using provided connection.
        /// </summary>
        /// <param name="connection">Destination of a source to export to.</param>
        /// <param name="collections">Collections of cards to export.</param>
        /// <returns></returns>
        Task<IExportResult> ExportAsync(string connection, IEnumerable<ICardCollection> collections);
    }
}
