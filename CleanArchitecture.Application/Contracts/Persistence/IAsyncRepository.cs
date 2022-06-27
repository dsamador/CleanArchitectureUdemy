using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;


namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        //Lista de todos los records de una entidad
        Task<IReadOnlyList<T>> GetAllAsync();
        
        //Devuelve una colección con una condición
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

        //ordenamiento de resultados
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString=null,
                                        bool disableTracking=true);

        //para usar la paginacion
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);

        //consulta por id
        Task<T> GetByIdAsync(int id);
        
        //Agregar un record
        Task<T> AddAsync(T entity);
        
        //actualizar un record
        Task<T> UpdateAsync(T entity);

        //Para borrar
        Task DeleteAsync(T entity);

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);       
    }
}
