using AutoMapper;
using API.Data.Entities;
using API.Data.Repositories.IntAdministrationRepository.Interfaces;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    public class CatalogDomain
    {
        private readonly ICatalogRepository _catalogRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CatalogDomain(
            ICatalogRepository catalogRepo,
            ICategoryRepository categoryRepo,
            IMapper mapper)
        {
            _catalogRepo = catalogRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<Result<Catalog>> CreateCatalogAsync(Catalog model)
        {
            try
            {
                model.CatalogStatus = true;
                var entity = _mapper.Map<CatalogEntity>(model);
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                var savedEntity = await _catalogRepo.AddAsync(entity);

                // Asignar categorías
                foreach (var category in model.Categories)
                {
                    if (!await _categoryRepo.ExistsAsync(category.CategoryId))
                        return Result<Catalog>.Failure($"Categoría {category.CategoryId} no encontrada.");

                    await _catalogRepo.AddCatalogCategoryAsync(new CatalogCategoryEntity
                    {
                        CatalogId = savedEntity.CatalogId,
                        CategoryId = category.CategoryId
                    });
                }


                var fullEntity = await _catalogRepo.GetByIdAsync(savedEntity.CatalogId);
                return Result<Catalog>.Success(_mapper.Map<Catalog>(fullEntity));
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Error al crear catálogo: {ex.Message}");
            }
        }

        public async Task<Result<Catalog>> UpdateCatalogAsync(int id, Catalog model)
        {
            try
            {
                var entity = await _catalogRepo.GetByIdAsync(id);
                if (entity == null)
                    return Result<Catalog>.Failure("Catálogo no encontrado.");

                // Mapear cambios básicos (nombre, descripción, etc.)
                _mapper.Map(model, entity);
                await _catalogRepo.UpdateAsync(entity);

                // Obtener categorías existentes
                var existing = await _catalogRepo.GetCatalogCategoriesAsync(id);
                var existingIds = existing.Select(cc => cc.CategoryId).ToHashSet();

                // Extraer IDs de las categorías entrantes
                var incomingIds = model.Categories.Select(c => c.CategoryId).ToHashSet();

                // Remover relaciones eliminadas
                foreach (var cc in existing.Where(x => !incomingIds.Contains(x.CategoryId)))
                {
                    await _catalogRepo.RemoveCatalogCategoryAsync(cc);
                }

                // Agregar nuevas relaciones
                foreach (var newId in incomingIds.Except(existingIds))
                {
                    if (!await _categoryRepo.ExistsAsync(newId))
                        return Result<Catalog>.Failure($"Categoría {newId} no encontrada.");

                    await _catalogRepo.AddCatalogCategoryAsync(new CatalogCategoryEntity
                    {
                        CatalogId = id,
                        CategoryId = newId
                    });
                }

                // Leer catálogo actualizado
                var updated = await _catalogRepo.GetByIdAsync(id);
                return Result<Catalog>.Success(_mapper.Map<Catalog>(updated));
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Error al actualizar catálogo: {ex.Message}");
            }
        }


        public async Task<Result<Catalog>> GetCatalogByIdAsync(int id)
        {
            try
            {
                var entity = await _catalogRepo.GetByIdAsync(id);
                return entity == null
                    ? Result<Catalog>.Failure("Catálogo no encontrado.")
                    : Result<Catalog>.Success(_mapper.Map<Catalog>(entity));
            }
            catch (Exception ex)
            {
                return Result<Catalog>.Failure($"Error al obtener catálogo: {ex.Message}");
            }
        }

        public async Task<Result<List<Catalog>>> GetAllCatalogsAsync()
        {
            try
            {
                var entities = await _catalogRepo.GetAllAsync();
                return Result<List<Catalog>>.Success(_mapper.Map<List<Catalog>>(entities));
            }
            catch (Exception ex)
            {
                return Result<List<Catalog>>.Failure($"Error al listar catálogos: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteCatalogAsync(int id)
        {
            try
            {
                var entity = await _catalogRepo.GetByIdAsync(id);
                if (entity == null) return Result<bool>.Failure("Catálogo no encontrado.");

                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
                await _catalogRepo.UpdateAsync(entity);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al eliminar catálogo: {ex.Message}");
            }
        }

        public async Task<Result<bool>> AddCategoriesToCatalogAsync(int id, List<int> catIds)
        {
            try
            {
                foreach (var cid in catIds)
                {
                    var category = await _categoryRepo.GetByIdAsync(cid);
                    if (category == null)
                        return Result<bool>.Failure($"Categoría {cid} no encontrada.");

                    await _catalogRepo.AddCatalogCategoryAsync(new CatalogCategoryEntity
                    {
                        CatalogId = id,
                        CategoryId = cid,
                        CategoryName = category.CategoryName
                    });
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al agregar categorías: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RemoveCategoryFromCatalogAsync(int id, int cid)
        {
            try
            {
                await _catalogRepo.RemoveCatalogCategoryAsync(new CatalogCategoryEntity
                {
                    CatalogId = id,
                    CategoryId = cid
                });

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error al quitar categoría: {ex.Message}");
            }
        }
    }
}
