namespace SimpleWildberriesSearcher.Core.Models
{
    public interface ICardCollection
    {
        /// <summary>
        /// Name of a collection (basically, name of category).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Collection of cards.
        /// </summary>
        List<ICard> Cards { get; }
    }
}
