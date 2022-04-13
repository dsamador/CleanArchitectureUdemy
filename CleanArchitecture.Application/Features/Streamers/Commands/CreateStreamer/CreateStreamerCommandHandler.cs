using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;//Para hacer los registros
        
        private readonly IMapper _mapper;
        
        //para que se mande un correo por cada nuevo streamer
        private readonly IEmailService _emailService;
        
        //para registrar la transaccion
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(
            IStreamerRepository streamerRepository, 
            IMapper mapper, 
            IEmailService emailService, 
            ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, 
            CancellationToken cancellationToken)
        {
            var streamerEntity = _mapper.Map<Streamer>(request);

            var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exitosamente");

            await SendEmail(newStreamer);

            return newStreamer.Id;

        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "dsamador@misena.edu.co"
                ,Body = "La empresa de Streamer se creo correctamente"
                ,Subject = "Mensaje de alerta"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError($"No se pudo enviar el correo exitosamente" +
                    $" de {streamer.Id}");
            }
        }
    }
}
