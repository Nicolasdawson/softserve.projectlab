using System.Collections.Generic;
using API.Models.IntAdmin.Interfaces;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.IntAdmin
{
    public class Category : ICategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool CategoryStatus { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public Category(int categoryId, string categoryName, bool categoryStatus, List<Item> items)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryStatus = categoryStatus;
            Items = items;
        }

        // Default constructor (no extra comments)
        public Category() { }

        // CRUD methods (with brief comments)

        /// <summary>
        /// Adds a new category.
        /// </summary>
        public Result<ICategory> AddCategory(ICategory category)
        {
            return Result<ICategory>.Success(category);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        public Result<ICategory> UpdateCategory(ICategory category)
        {
            return Result<ICategory>.Success(category);
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        public Result<ICategory> GetCategoryById(int categoryId)
        {
            var category = new Category(categoryId, "Example Category", true, new List<Item>());
            return Result<ICategory>.Success(category);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        public Result<List<ICategory>> GetAllCategories()
        {
            var categories = new List<ICategory>
            {
                new Category(1, "Category 1", true, new List<Item>()),
                new Category(2, "Category 2", false, new List<Item>())
            };
            return Result<List<ICategory>>.Success(categories);
        }

        /// <summary>
        /// Removes a category by its ID.
        /// </summary>
        public Result<bool> RemoveCategory(int categoryId)
        {
            return Result<bool>.Success(true);
        }

        // Methods from the diagram (minimal comments)

        /// <summary>
        /// Adds an item to the category's Items list.
        /// </summary>
        public Result<bool> AddItemToCategory()
        {
            // Example logic (adjust if you need a parameter for the item)
            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Removes an item from the category's Items list.
        /// </summary>
        public Result<bool> RemoveItemFromCategory()
        {
            // Example logic (adjust if you need a parameter for the item or SKU)
            return Result<bool>.Success(true);
        }
    }
}
