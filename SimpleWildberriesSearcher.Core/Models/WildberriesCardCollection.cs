namespace SimpleWildberriesSearcher.Core.Models
{
    public class WildberriesCardCollection : ICardCollection
    {
        #region Constructors
        public WildberriesCardCollection()
        { }

        public WildberriesCardCollection(string name)
        {
            Name = name;
        }
        #endregion

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public List<ICard> Cards { get; set; } = new List<ICard>();
    }
}
