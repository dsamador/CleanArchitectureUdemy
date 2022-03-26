using CleanArchitecture.Domain;
namespace CleanArchitecture.Application.
            Contracts.Persistence
{
    public interface IVideoRepository : 
            IAsyncRepository<Video>
    {
        //Consulta por nombre del video
        Task<IEnumerable<Video>> GetVideoByNombre
                (string nombreVideo);
        
        //Consulta por el usuario que tiene el video
        Task<Video> GetVideoByUserName
                (string username);
    }
}
