using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommand : IRequest
    {//para eliminar solo necesitamos el Id
        public int Id { get; set; }
    }
}
