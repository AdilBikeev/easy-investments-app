namespace Stock.API.DTOs
{
    public record MoneyValueDTO
    {
        public string Currency { get; init; }
        public long Units { get; init; }
        public int Nano { get; init; }

        /// <summary>
        /// Максимальное число для дробной части номинала.
        /// </summary>
        private const decimal NanoFactor = 1_000_000_000;

        public static implicit operator decimal(MoneyValueDTO value)
        {
            return value.Units + value.Nano / NanoFactor;
        }
    }
}
