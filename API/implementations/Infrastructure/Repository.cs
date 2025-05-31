using API.Abstractions;
using API.Helpers;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using API.implementations.Infrastructure.Data;

namespace API.implementations.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal AppDbContext _context;
    internal DbSet<TEntity> _entity;

    public Repository(AppDbContext context)
    {
        _context = context;
        _entity = context.Set<TEntity>();
    }

    public virtual async Task<ActionResponseDTO<TEntity>> AddAsync(TEntity entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponseDTO<TEntity>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        _entity.Remove(row);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = true,
            };
        }
        catch
        {
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = false,
                Message = "ERR002"
            };
        }
    }


    public virtual async Task<ActionResponseDTO<TEntity>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }
        return new ActionResponseDTO<TEntity>
        {
            WasSuccess = true,
            Result = row
        };
    }

    public virtual async Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync()
    {
        return new ActionResponseDTO<IEnumerable<TEntity>>
        {
            WasSuccess = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ActionResponseDTO<TEntity>> UpdateAsync(TEntity entity)
    {
        _context.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponseDTO<TEntity>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponseDTO<IEnumerable<TEntity>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponseDTO<IEnumerable<TEntity>>
        {
            WasSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }
    
    public virtual async Task<ActionResponseDTO<int>> GetTotalRecordsAsync()
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponseDTO<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }
    
    private ActionResponseDTO<TEntity> ExceptionActionResponse(Exception exception)
    {
        return new ActionResponseDTO<TEntity>
        {
            WasSuccess = false,
            Message = exception.Message
        };
    }

    private ActionResponseDTO<TEntity> DbUpdateExceptionActionResponse()
    {
        return new ActionResponseDTO<TEntity>
        {
            WasSuccess = false,
            Message = "ERR003"
        };
    }
}

