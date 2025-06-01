using API.DTO;
using Ardalis.Specification;

namespace API.Abstractions;

public interface IRepository<T> where T : class
{
    Task<ActionResponseDTO<T>> GetAsync(int id);

    Task<ActionResponseDTO<IEnumerable<T>>> GetAsync();

    Task<ActionResponseDTO<T>> AddAsync(T entity);

    Task<ActionResponseDTO<T>> DeleteAsync(int id);

    Task<ActionResponseDTO<T>> UpdateAsync(T entity);

    Task<ActionResponseDTO<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponseDTO<int>> GetTotalRecordsAsync();
}
