using API.Models;
using API.Models.IntAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Implementations.Domain
{
    /// <summary>
    /// Domain class for handling Category operations.
    /// Uses in-memory storage for categories.
    /// </summary>
    public class CategoryDomain
    {
        // In-memory storage for categories
        private readonly List<Category> _categories = new List<Category>();

        /// <summary>
        /// Creates a new category and adds it to the in-memory list.
        /// </summary>
        /// <param name="category">Category object to be created</param>
        /// <returns>Result object containing the created category</returns>
        public async Task<Result<Category>> CreateCategory(Category category)
        {
            try
            {
                _categories.Add(category);
                return Result<Category>.Success(category);
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Failed to create category: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing category if found.
        /// </summary>
        /// <param name="category">Category object with updated information</param>
        /// <returns>Result object containing the updated category</returns>
        public async Task<Result<Category>> UpdateCategory(Category category)
        {
            try
            {
                var existingCategory = _categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                if (existingCategory != null)
                {
                    existingCategory.CategoryName = category.CategoryName;
                    existingCategory.CategoryStatus = category.CategoryStatus;
                    existingCategory.Items = category.Items;
                    return Result<Category>.Success(existingCategory);
                }
                else
                {
                    return Result<Category>.Failure("Category not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Failed to update category: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a category by its unique ID.
        /// </summary>
        /// <param name="categoryId">Unique identifier of the category</param>
        /// <returns>Result object containing the category if found, otherwise an error</returns>
        public async Task<Result<Category>> GetCategoryById(int categoryId)
        {
            try
            {
                var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
                return category != null
                    ? Result<Category>.Success(category)
                    : Result<Category>.Failure("Category not found.");
            }
            catch (Exception ex)
            {
                return Result<Category>.Failure($"Failed to get category: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all categories stored in memory.
        /// </summary>
        /// <returns>Result object containing a list of categories</returns>
        public async Task<Result<List<Category>>> GetAllCategories()
        {
            try
            {
                return Result<List<Category>>.Success(_categories);
            }
            catch (Exception ex)
            {
                return Result<List<Category>>.Failure($"Failed to retrieve categories: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes a category by its unique ID.
        /// </summary>
        /// <param name="categoryId">Unique identifier of the category to remove</param>
        /// <returns>Result object indicating success or failure</returns>
        public async Task<Result<bool>> RemoveCategory(int categoryId)
        {
            try
            {
                var categoryToRemove = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
                if (categoryToRemove != null)
                {
                    _categories.Remove(categoryToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Category not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove category: {ex.Message}");
            }
        }
    }
}
