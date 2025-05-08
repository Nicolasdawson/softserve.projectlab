using AutoMapper;
using API.Implementations.Domain;
using API.Models.IntAdmin;
using softserve.projectlabs.Shared.DTOs.Category;
using softserve.projectlabs.Shared.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.IntAdmin
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryDomain _categoryDomain;
        private readonly IMapper _mapper;

        public CategoryService(CategoryDomain categoryDomain, IMapper mapper)
        {
            _categoryDomain = categoryDomain;
            _mapper = mapper;
        }

        public async Task<Result<Category>> CreateCategoryAsync(CategoryCreateDto categoryDto)
        {
            var model = _mapper.Map<Category>(categoryDto);
            model.CategoryStatus = true; // o dejarlo en el dominio
            return await _categoryDomain.CreateCategoryAsync(model);
        }

        public async Task<Result<Category>> UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryDto)
        {
            var model = _mapper.Map<Category>(categoryDto);
            return await _categoryDomain.UpdateCategoryAsync(categoryId, model);
        }

        public Task<Result<Category>> GetCategoryByIdAsync(int categoryId)
        {
            return _categoryDomain.GetCategoryByIdAsync(categoryId);
        }

        public Task<Result<List<Category>>> GetAllCategoriesAsync()
        {
            return _categoryDomain.GetAllCategoriesAsync();
        }

        public Task<Result<bool>> DeleteCategoryAsync(int categoryId)
        {
            return _categoryDomain.DeleteCategoryAsync(categoryId);
        }
    }
}
