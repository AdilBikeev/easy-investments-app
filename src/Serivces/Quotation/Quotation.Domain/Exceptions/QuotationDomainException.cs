namespace Quotation.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class QuotationDomainException : Exception
    {
        public QuotationDomainException()
        { }

        public QuotationDomainException(string message)
            : base(message)
        { }

        public QuotationDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
