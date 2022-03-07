namespace Quotation.API.Model
{
    /// <summary>
    /// Информация со списком существующих валют.
    /// </summary>
    [Serializable]
    [XmlRoot("ValuteData")]
    public sealed record ValuteData
    {
        /// <summary>
        /// Список существующих валют.
        /// </summary>
        [XmlElement(ElementName = "EnumValutes")]
        public List<EnumValutes> EnumValutes { get; init; }
    }
}
