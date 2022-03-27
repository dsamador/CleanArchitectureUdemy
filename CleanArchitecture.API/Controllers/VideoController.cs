using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        //Comunicacion de capa Presentation (API) a capa Application
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //lista de videos por usuario por ello username, GetVideo se refiere al nombre del metodo
        //para el cliente dentro de la URL
        [HttpGet("{username}", Name = "GetVideo")]
        //El tipo que vamos a devolver y un estatus code de la respuesta
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
        {
            //Instancia del objeto query
            var query = new GetVideosListQuery(username);
            //Enviamos el objeto query a la capa de Application
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }
    }
}
