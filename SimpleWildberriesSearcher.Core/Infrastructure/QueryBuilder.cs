using System.Text;

namespace SimpleWildberriesSearcher.Core.Infrastructure
{
    /// <summary>
    /// Provides static methods to build request urls.
    /// </summary>
    internal static class QueryBuilder
    {
        /// <summary>
        /// Builds a full request path based on base route and query parameters.
        /// </summary>
        /// <param name="route">Base route of a request.</param>
        /// <param name="items"><see cref="IDictionary{TKey, TValue}"/> of query parametes, 
        /// where key is a parameter name, and value is a parameter value.</param>
        public static string BuildQuery(string route, IDictionary<string, string> items)
        {
            var firstKey = items.First().Key;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(route);
            stringBuilder.Append('?');

            foreach (var item in items)
            {
                if (item.Key != firstKey)
                    stringBuilder.Append('&');

                stringBuilder.Append(item.Key);
                stringBuilder.Append('=');
                stringBuilder.Append(item.Value);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Builds a full request path based on base route with interpolated id of an item to get.
        /// </summary>
        /// <param name="route">Base route of a request.</param>
        /// <param name="id">Id of an item to get.</param>
        /// <returns></returns>
        public static string BuildQuery(string route, string id)
        {
            try
            {
                return string.Format(route, id);
            }
            catch
            {
                throw new InvalidOperationException("Error during building request from an interpolated route template.");
            }
        }
    }
}
