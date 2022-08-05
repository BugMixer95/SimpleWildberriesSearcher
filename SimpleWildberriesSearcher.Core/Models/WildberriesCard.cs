namespace SimpleWildberriesSearcher.Core.Models
{
    internal class WildberriesCard : ICard
    {
        private string _name = string.Empty;
        private string _brand = string.Empty;

        internal string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <inheritdoc />
        public string Title
        {
            get => string.Join(' ', _brand, '/', _name);
        }

        /// <inheritdoc />
        public string Brand
        {
            get => _brand;
            set => _brand = value;
        }

        /// <inheritdoc />
        public string Id { get; set; } = string.Empty;

        /// <inheritdoc />
        public int Feedbacks { get; set; }

        /// <inheritdoc />
        public double Price { get; set; } 
    }
}
