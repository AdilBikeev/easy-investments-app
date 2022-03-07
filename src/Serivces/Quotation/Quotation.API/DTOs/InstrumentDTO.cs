namespace Quotation.API.DTOs
{
    /// <summary>
    /// Информация об инструменте (акция, облигация и т.п.).
    /// </summary>
    public record InstrumentDTO
    {
        /// <summary>
        /// Идентификатор, позволяющий определить актив в зависимости от его типа (акция, облигация, валюта и т.д.), площадки и рынка.
        /// </summary>
        public string Figi { get; init; }

        /// <summary>
        /// Краткое наименование инструмента.
        /// </summary>
        public string Ticker { get; init; }

        /// <summary>
        /// Уникальный код для Ticker, позволяющий уточнить конкретный  Ticker инструмента, дабы не попасть на дубли.
        /// </summary>
        /// <example>GAZP - Облигации Газпрома.</example>
        public string ClassCode { get; init; }

        /// <summary>
        /// Международный идентификационный номер ценных бумаг.
        /// </summary>
        public string ISIN { get; init; }

        /// <summary>
        /// Код отображаемой валюты.
        /// </summary>
        public string Currency { get; init; }
    }
}
