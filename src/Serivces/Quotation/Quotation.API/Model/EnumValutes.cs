namespace Quotation.API.Model
{
    /// <summary>
    /// Информация о валюте.
    /// </summary>
    [Serializable]
    public sealed record EnumValutes
    {
        /// <summary>
        /// Внутренний код валюты.
        /// </summary>
        /// <remarks>нутренний код — код для идентификации валют, является локальным и уникальным идентификатором валюты в данной базе, необходим для использования в качестве параметра для методов GetCursDynamic (GetCursDynamicXML).</remarks>
        public string Vcode { get; init; }

        /// <summary>
        /// Название валюты.
        /// </summary>
        public string Vname { get; init; }

        /// <summary>
        /// Англ. название валюты.
        /// </summary>
        public string VEngname { get; init; }

        /// <summary>
        /// Номинал.
        /// </summary>
        public string Vnom { get; init; }

        /// <summary>
        /// Внутренний код валюты, являющейся ’базовой’.
        /// </summary>
        /// <remarks>Этот код используется для связи, при изменениях кодов или названий фактически одной и той же валюты.</remarks>
        public string VcommonCode { get; init; }

        /// <summary>
        /// Цифровой код ISO.
        /// </summary>
        public string VnumCode { get; init; }

        /// <summary>
        /// 3х буквенный код ISO.
        /// </summary>
        public string VcharCode { get; init; }
    }
}
