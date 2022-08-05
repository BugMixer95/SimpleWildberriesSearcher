using SimpleWildberriesSearcher.Core.Models;

namespace SimpleWildberriesSearcher.Core.Services.SearchService
{
    /// <summary>
    /// Provides a contract to search cards.
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Asynchronously collects cards based on provided categories.
        /// </summary>
        /// <param name="items">Collection of categories to search for.</param>
        Task<IEnumerable<ICardCollection>> SearchAsync(IEnumerable<string> categories);
    }
}
