namespace Stock.API.DTOs
{
    /// <summary>
    /// Информация о выплатах по котировке компаанией.
    /// </summary>
    public record PaymentsInfoDTO
    {
        /// <summary>
        /// Cумма выплат.
        /// </summary>
        public decimal AmountPayments { get; init; }

        /// <summary>
        /// Кол-во выплат.
        /// </summary>
        public int QuantityPayments { get; init; }

        /// <summary>
        /// Средняя величина одной выплаты.
        /// </summary>
        public decimal AvgPayoutAmount { get; init; }
    }
}
