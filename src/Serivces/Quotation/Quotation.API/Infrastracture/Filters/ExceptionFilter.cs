using Quotation.API.Infrastracture.Exceptions;
using Quotation.API.Infrastracture.Extensions;

namespace Quotation.API.Infrastracture.Filters
{
    /// <summary>
    /// Глобальный фильтр исключений.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// No comments.
        /// </summary>
        public ExceptionFilter()
        {

        }

        /// <summary>
        /// Событие отлавливания исключения.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            IActionResult? result = context.Exception switch
            {
                ApiException apiExceptions => new ObjectResult(context.GetProblemDetails()) { StatusCode = apiExceptions.StatusCode },
                _ => null
            };

            if (result == null)
                return;

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
