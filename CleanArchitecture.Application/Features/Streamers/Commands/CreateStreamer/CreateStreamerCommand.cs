using MediatR;

namespace CleanArchitecture.Application.
    Features.Streamers.Commands
{
    //devuelve el id del registro generado y representa el mapeo de la data
    //que manda el cliente luego hacemos la validacion 
    public class CreateStreamerCommand : IRequest<int>
    {
        public string Nombre 
            { get; set; } = string.Empty;
        public string Url 
            { get; set; } = string.Empty;
    }
}
