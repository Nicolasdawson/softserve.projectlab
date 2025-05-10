using API.Data;
using API.Data.Models;
using API.Data.Models.DTOs.Category;
using API.Data.Models.DTOs.User;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<bool> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var category = mapper.Map<Category>(categoryCreateDto);
                await db.Category.AddAsync(category);
                await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                var existCategory = await db.Category.FirstOrDefaultAsync(c => c.Id == id);

                if (existCategory != null)
                {
                    db.Category.Remove(existCategory);
                    await db.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {

                return false;
            }
        }

        public async Task<IEnumerable<CategoryGetDto>> GetCategories()
        {
            var categories = await db.Category.ToListAsync();
            var PelicuasDto = mapper.Map<List<CategoryGetDto>>(categories);
            return PelicuasDto;
        }
    }
}
