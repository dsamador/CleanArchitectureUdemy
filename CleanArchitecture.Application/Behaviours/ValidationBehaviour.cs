using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken 
            cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Buscamos si tenemos alguna validacion, si la hay hacemos la evaluacion de cada validacion
            //versus la propiedad del objeto request que manda el cliente
            if (_validators.Any())
            {
                //context es lo que manda el cliente
                var context = new ValidationContext<TRequest>(request);
                
                //Evaluamos cada validacion, por ejemplo el CreateStreamerCommandValidator.
                //Aqui creamos la logica que no solamente lea la clase mencionada sino tambien
                //la logica de UpdateStreamerCommandValidator y las ejecute

                var validationResults = await Task.WhenAll(_validators
                    .Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if(failures.Count != 0)
                {
                    throw new ValidationException(failures);//clase que creamos anteriormente
                }
            }

            return await next();
        }
    }
}
