using CleanArchitecture.Domain;
namespace CleanArchitecture.Application.
            Contracts.Persistence
{
    public interface IVideoRepository : 
            IAsyncRepository<Video>
    {
        //Consulta por nombre del video
        Task<Video> GetVideoByNombre
                (string nombreVideo);
        
        //Consulta por el usuario que tiene el video
        Task<IEnumerable<Video>> GetVideoByUserName
                (string username);
    }
}
