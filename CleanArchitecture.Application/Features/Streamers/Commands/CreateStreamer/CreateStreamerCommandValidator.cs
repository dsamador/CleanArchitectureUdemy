using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandValidator : 
        AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(p => p.Nombre)//Evaluacion de la propiedad
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")//mensaje en caso de no cumplir
                .NotNull()
                .MaximumLength(50).WithMessage("{Nombre} no puede exeder los 50 caracteres");
            
            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("La {Url} no puede estar en blanco");
        }
    }
}
