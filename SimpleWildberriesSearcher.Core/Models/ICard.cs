namespace SimpleWildberriesSearcher.Core.Models
{
    public interface ICard
    {
        /// <summary>
        /// Title of a card.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Producer's name of a card item.
        /// </summary>
        string Brand { get; }

        /// <summary>
        /// Id of a card.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Amount of feedbacks given to a card.
        /// </summary>
        int Feedbacks { get; }

        /// <summary>
        /// Price of a card item (without discount).
        /// </summary>
        double Price { get; }
    }
}
