using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Item;
using softserve.projectlabs.Shared.Utilities;
using AutoMapper;

namespace API.Implementations.Domain;

public class ItemDomain
{
    private readonly IItemRepository _itemRepo;
    private readonly IMapper _mapper;

    public ItemDomain(IItemRepository itemRepo, IMapper mapper)
    {
        _itemRepo = itemRepo;
        _mapper = mapper;
    }

    public async Task<Result<Item>> CreateItemAsync(ItemCreateDto dto)
    {
        try
        {
            var model = _mapper.Map<Item>(dto);

            model.CurrentStock = model.OriginalStock;
            model.ItemStatus = true;

            var entity = _mapper.Map<ItemEntity>(model);
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _itemRepo.AddAsync(entity);
            var result = _mapper.Map<Item>(saved);
            return Result<Item>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<Item>.Failure($"Error creating item: {ex.Message}");
        }
    }

    public async Task<Result<Item>> UpdateItemAsync(int id, ItemDto dto)
    {
        try
        {
            var entity = await _itemRepo.GetByIdAsync(id);
            if (entity == null)
                return Result<Item>.Failure("Item not found.");

            _mapper.Map(dto, entity);
            await _itemRepo.UpdateAsync(entity);

            var domainModel = _mapper.Map<Item>(entity);
            return Result<Item>.Success(domainModel);
        }
        catch (Exception ex)
        {
            return Result<Item>.Failure($"Error updating item: {ex.Message}");
        }
    }

    public async Task<Result<Item>> GetItemByIdAsync(int id)
    {
        try
        {
            var entity = await _itemRepo.GetByIdAsync(id);
            if (entity == null)
                return Result<Item>.Failure("Item not found.");

            var domainModel = _mapper.Map<Item>(entity);
            return Result<Item>.Success(domainModel);
        }
        catch (Exception ex)
        {
            return Result<Item>.Failure($"Error retrieving item: {ex.Message}");
        }
    }

    public async Task<Result<List<Item>>> GetAllItemsAsync()
    {
        try
        {
            var entities = await _itemRepo.GetAllAsync();
            var domainModels = _mapper.Map<List<Item>>(entities);
            return Result<List<Item>>.Success(domainModels);
        }
        catch (Exception ex)
        {
            return Result<List<Item>>.Failure($"Error retrieving items: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteItemAsync(int id)
    {
        try
        {
            var deleted = await _itemRepo.DeleteAsync(id);
            return deleted
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Item not found.");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error deleting item: {ex.Message}");
        }
    }

    public async Task<Result<bool>> UpdatePriceAsync(int id, decimal newPrice)
    {
        try
        {
            var entity = await _itemRepo.GetByIdAsync(id);
            if (entity == null)
                return Result<bool>.Failure("Item not found.");

            entity.ItemPrice = newPrice;
            await _itemRepo.UpdateAsync(entity);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error updating price: {ex.Message}");
        }
    }

    public async Task<Result<bool>> UpdateDiscountAsync(int id, decimal? newDiscount)
    {
        try
        {
            var entity = await _itemRepo.GetByIdAsync(id);
            if (entity == null)
                return Result<bool>.Failure("Item not found.");

            entity.ItemDiscount = newDiscount;
            await _itemRepo.UpdateAsync(entity);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Error updating discount: {ex.Message}");
        }
    }
}
