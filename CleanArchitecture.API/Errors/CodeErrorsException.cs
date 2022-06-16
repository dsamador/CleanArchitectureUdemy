namespace CleanArchitecture.API.Errors
{
    public class CodeErrorsException : CodeErrorResponse
    {
        public string? Details { get; set; }
        public CodeErrorsException(
            int statusCode, string? message = null, string? details = null) 
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
