using AutoMapper;
using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using softserve.projectlabs.Shared.DTOs.Category;

namespace API.Implementations.Domain
{
    public class CategoryDomain
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryDomain(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<Result<Category>> CreateCategoryAsync(Category model)
        {
            try
            {
                model.CategoryStatus = true;

                var entity = _mapper.Map<CategoryEntity>(model);
                var saved = await _categoryRepo.AddAsync(entity);
                return Result<Category>.Success(_mapper.Map<Category>(saved));
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Error al crear categoría: {ex.Message}");
            }
        }

        public async Task<Result<Category>> UpdateCategoryAsync(int id, Category model)
        {
            try
            {
                var entity = await _categoryRepo.GetByIdAsync(id);
                if (entity == null)
                    return Result<Category>.Failure("Categoría no encontrada.");

                _mapper.Map(model, entity);

                var updated = await _categoryRepo.UpdateAsync(entity);
                return Result<Category>.Success(_mapper.Map<Category>(updated));
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Error al actualizar categoría: {ex.Message}");
            }
        }

        public async Task<Result<Category>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var entity = await _categoryRepo.GetByIdAsync(id);
                return entity == null
                    ? Result<Category>.Failure("Categoría no encontrada.")
                    : Result<Category>.Success(_mapper.Map<Category>(entity));
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Error al obtener categoría: {ex.Message}");
            }
        }

        public async Task<Result<List<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var entities = await _categoryRepo.GetAllAsync();
                return Result<List<Category>>.Success(_mapper.Map<List<Category>>(entities));
            }
            catch (Exception ex)
            {
                return Result<List<Category>>.Failure($"Error al listar categorías: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var entity = await _categoryRepo.GetByIdAsync(id);
                if (entity == null)
                    return Result<bool>.Failure("Categoría no encontrada.");

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                await _categoryRepo.UpdateAsync(entity);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar categoría: {ex.Message}");
            }
        }
    }
}
