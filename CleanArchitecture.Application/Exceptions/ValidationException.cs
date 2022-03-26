using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        //esta excepcion va a manejar todas las validaciones necesarias
        //y las agrupa
        public ValidationException() : base("Se presentaron uno o mas errores de validacion")
        {
            Errors = new Dictionary<string, string[]>();
        }

        //todas las excepciones van a ser leidas con esta clase y van a ser leidas con este constructor
        //haciendo que los resultados de las excepciones se seteen dentro de la propiedad errors
        //que es finalmente el valor que se devolvera al cliente.
        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; }
    }
}
