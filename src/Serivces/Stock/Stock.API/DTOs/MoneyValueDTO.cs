namespace Stock.API.DTOs
{
    public record MoneyValueDTO
    {
        public string Currency { get; init; }
        public long Units { get; init; }
        public int Nano { get; init; }
    }
}
