namespace SimpleWildberriesSearcher.Core.Models
{
    public class WildberriesCardCollection : ICardCollection
    {
        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public List<ICard> Cards { get; set; } = new List<ICard>();
    }
}
