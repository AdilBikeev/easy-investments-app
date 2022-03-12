namespace Quotation.API.Application.Commands
{
    /// <summary>
    /// Бизнес команда для создания или обновления инфомации по котировке
    /// в БД.
    /// </summary>
    public class CreateQuotationCommand
        : IRequest<bool>
    {
        /// <summary>
        /// FIGI идентификатор котировки.
        /// </summary>
        public string FIGI { get; private set; }

        /// <summary>
        /// Наименование котировки.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Краткое название в биржевой информации котируемых инструментов (акций, облигаций, индексов).
        /// </summary>
        public string? Ticker { get; set; }

        public CreateQuotationCommand(string fIGI, string name, string? ticker)
        {
            FIGI = fIGI;
            Name = name;
            Ticker = ticker;
        }
    }
}
