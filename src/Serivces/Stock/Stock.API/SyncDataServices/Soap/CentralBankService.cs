using System.Xml;

using CentralBankDailyInfoService;

namespace Stock.API.SyncDataServices.Soap
{
    public interface ICentralBankService
    {
        /// <summary>
        /// Возвращает список валют.
        /// </summary>
        /// <param name="seld">
        /// True - перечень ежемесячных валют
        /// False — перечень ежедневных валют
        /// </param>
        Task<IEnumerable<EnumValutes>> GetEnumValutes(bool seld = false);
    }

    /// TODO: Вынести в отдельную библиотеку NuGet и опубликовать как неофициальный SDK.
    public class CentralBankService : ICentralBankService
    {
        /// <summary>
        /// Сервис ЦБ РФ для работы с валютой.
        /// </summary>
        private readonly CentralBankDailyInfoService.DailyInfoSoap _cbService = new DailyInfoSoapClient(
                            DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap12
            );

        public CentralBankService()
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<EnumValutes>> GetEnumValutes(bool seld = false)
        {
            var resp = await _cbService.EnumValutesXMLAsync(new EnumValutesXMLRequest(seld));
            ValuteData valuteData;

            if (resp is null)
                throw new Exception($"Неполадки при работе с сервисом {nameof(GetEnumValutes)}: сервис ничего не ответил");

            var respStr = resp.EnumValutesXMLResult.OuterXml;
            using (var reader = new StringReader(respStr))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ValuteData));
                valuteData = (ValuteData)serializer.Deserialize(reader);
            }

            return valuteData!.EnumValutes;
        }
    }
}
