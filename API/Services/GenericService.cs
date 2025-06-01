using API.Abstractions;
using API.DTO;

namespace API.Services;

public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
{
    private readonly IRepository<TEntity> _repository;

    public GenericService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<ActionResponseDTO<TEntity>> AddAsync(TEntity model) => await _repository.AddAsync(model);

    public virtual async Task<ActionResponseDTO<TEntity>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

    public virtual async Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync() => await _repository.GetAsync();

    public virtual async Task<ActionResponseDTO<TEntity>> GetAsync(int id) => await _repository.GetAsync(id);

    public virtual async Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

    public virtual async Task<ActionResponseDTO<int>> GetTotalRecordsAsync() => await _repository.GetTotalRecordsAsync();

    public virtual async Task<ActionResponseDTO<TEntity>> UpdateAsync(TEntity model) => await _repository.UpdateAsync(model);
}
