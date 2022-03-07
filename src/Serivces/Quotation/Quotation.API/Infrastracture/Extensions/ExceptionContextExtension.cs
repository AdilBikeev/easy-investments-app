namespace Quotation.API.Infrastracture.Extensions
{
    /// <summary>
    /// Перенести в отдельную библиотеку Nuget.
    /// </summary>
    public static class ExceptionContextExtension
    {
        /// <summary>
        /// Возвращает подробности об ошибки включая traceId.
        /// </summary>
        public static ProblemDetails GetProblemDetails(this ExceptionContext context)
        {
            string value = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
            var problemDetails = new ProblemDetails
            {
                Detail = context.Exception.Message
            };
            problemDetails.Extensions["traceId"] = value;
            return problemDetails;
        }
    }
}
