using API.DTO;

namespace API.Services;

public interface IGenericService<TEntity> where TEntity : class
{
    Task<ActionResponseDTO<TEntity>> GetAsync(int id);

    Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync();

    Task<ActionResponseDTO<TEntity>> AddAsync(TEntity entity);

    Task<ActionResponseDTO<TEntity>> DeleteAsync(int id);

    Task<ActionResponseDTO<TEntity>> UpdateAsync(TEntity entity);

    Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponseDTO<int>> GetTotalRecordsAsync();
}
