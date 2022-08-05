using Newtonsoft.Json.Linq;
using SimpleWildberriesSearcher.Core.Infrastructure;
using SimpleWildberriesSearcher.Core.Models;
using System.Collections.Concurrent;

namespace SimpleWildberriesSearcher.Core.Services.SearchService
{
    /// <summary>
    /// Service which is used to search cards at Wildberries store.
    /// </summary>
    public class WildberriesSearchService : ISearchService
    {
        private const string _searchAllRoute = @"https://search.wb.ru/exactmatch/ru/common/v4/search";
        private const string _cardMetadataRoute = @"https://wbx-content-v2.wbstatic.net/ru/{0}.json";

        /// <summary>
        /// Max number of tasks which could run in parallel. <br/>
        /// Yes, it's a magic number, but it's perfect to be used as an example.
        /// </summary>
        private const int _maxConcurrentTasks = 3;

        private readonly HttpClient _httpClient = new HttpClient();

        private object _locker = new();

        private Dictionary<string, string> _searchAllQueryParams = new()
        {
            // basically, WB uses these params in almost every their XHP request
            { "curr", "rub" },
            { "dest", "-1029256,-102269,-2162196,-1257786" },
            { "lang", "ru" },
            { "locale", "ru" },
            { "regions", "68,64,83,4,38,80,33,70,82,86,75,30,69,22,66,31,40,1,48,71" },

            // parameters which are specific to "Search all" request
            { "resultset", "catalog" },
            { "sort", "popular" },
            { "suppressSpellcheck", "false" },
            { "page", "1" },            // searching on a single page only
            { "query", string.Empty }   // this one would be overwritten at every new request
        };

        #region ISearchService Members
        /// <inheritdoc />
        public async Task<IEnumerable<ICardCollection>> SearchAsync(IEnumerable<string> categories)
        {
            List<ICardCollection> collections = new();
            ConcurrentQueue<ICardCollection> queue = new();

            await Parallel.ForEachAsync(
                categories,
                new ParallelOptions() { MaxDegreeOfParallelism = _maxConcurrentTasks },
                async (category, cancellationToken) =>
                {
                    ICardCollection collection = await GetCards(category);
                    queue.Enqueue(collection);
                });

            if (queue != null)
                collections = queue.ToList();

            return collections;
        }
        #endregion

        #region Assistants
        /// <summary>
        /// Asynchronously receives collection of cards for provided category.
        /// </summary>
        /// <param name="category">Category name.</param>
        private async Task<ICardCollection> GetCards(string category)
        {
            ICardCollection collection = new WildberriesCardCollection() { Name = category };

            string requestUri = string.Empty;

            lock (_locker)
            {
                _searchAllQueryParams["query"] = category;
                requestUri = QueryBuilder.BuildQuery(_searchAllRoute, _searchAllQueryParams);
            }

            JObject responseObj = await SendGetRequest(requestUri);
            if (responseObj == null)
                return new WildberriesCardCollection();

            var serializedItems = responseObj["data"]["products"].Children().ToList();

            foreach (var item in serializedItems)
            {
                try
                {
                    string itemId = item["id"].ToString();

                    ICard card = new WildberriesCard()
                    {
                        Id = itemId,
                        Name = item["name"].ToString(),
                        Brand = item["brand"].ToString(),
                        Feedbacks = int.Parse(item["feedbacks"].ToString()),
                        Price = double.Parse(item["priceU"].ToString().TrimEnd('0'))
                    };

                    collection.Cards.Add(card);
                }
                catch (NullReferenceException ex)
                {
                    throw ex;
                }
            }

            await Parallel.ForEachAsync(
                collection.Cards,
                new ParallelOptions() { MaxDegreeOfParallelism = _maxConcurrentTasks },
                async (card, cancellationToken) =>
                {
                    try
                    {
                        string name = await GetCardNameFromMetadata(card.Id);

                        if (name != null)
                            (card as WildberriesCard).Name = name;
                    }
                    catch (NullReferenceException)
                    {
                        // basically, here we just continue executing of a ForEach loop
                    }
                });

            return collection;
        }

        /// <summary>
        /// Asynchronously gets card name from card metadata file.
        /// </summary>
        /// <param name="id">Id of a card.</param>
        private async Task<string> GetCardNameFromMetadata(string id)
        {
            string cardName = string.Empty;

            string requestUri = QueryBuilder.BuildQuery(_cardMetadataRoute, id);

            JObject responseObj = await SendGetRequest(requestUri);
            if (responseObj == null)
                return string.Empty;

            cardName = responseObj["imt_name"]?.ToString();
            return cardName;
        }

        /// <summary>
        /// Sends a GET request to a provided Uri.
        /// </summary>
        private async Task<JObject> SendGetRequest(string uri)
        {
            var response = await _httpClient.GetStringAsync(uri);
            return JObject.Parse(response);
        }
        #endregion
    }
}
