using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers
    .Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler :
        IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommand> _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository streamerRepository, 
            IMapper mapper, ILogger<UpdateStreamerCommand> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, 
            CancellationToken cancellationToken)
        {
            //primero consultar si existe el registro
            var streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);
            if (streamerToUpdate == null)
            {
                _logger.LogError($"No se encontro el streamer id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            //request ------> streamerToUpdate, id, nombre, url
                       //origen  destino           tipo de dato                   destino
            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            //ahora enviamos la data que se va a actualizar
            await _streamerRepository.UpdateAsync(streamerToUpdate);
            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");
            
            return Unit.Value;
        }
    }
}
