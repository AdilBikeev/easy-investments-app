namespace Quotation.API.Infrastracture.Exceptions
{
    /// <summary>
    /// Для отлавливания исключений относящихся к API.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Статус код запроса.
        /// </summary>
        public int StatusCode { get; init; }

        /// <summary>
        /// Ответ от сервера.
        /// </summary>
        public string? Response { get; init; }

        /// <summary>
        /// Инициализация ошибки связанная с взаимодействием с API сервера.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="statusCode">Статус код запроса.</param>
        /// <param name="response">Ответ от сервера.</param>
        public ApiException(string message, int statusCode, string? response = default) : base(message)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}
