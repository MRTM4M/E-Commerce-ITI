using E_commerce_iti.Data;
using E_commerce_iti.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceDB _context;

        public CategoryService(ECommerceDB context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                        .Include(c => c.Products)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            category.Name = category.Name.Trim();

            _context.Categories.Add(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new ArgumentException("Category already exists.");
            }
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory == null)
                return;

            bool categoryExists = await _context.Categories
                .AnyAsync(c =>
                    c.Id != category.Id &&
                    c.Name.ToLower() == category.Name.ToLower());

            if (categoryExists)
                throw new ArgumentException(
                    "Category already exists.");

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.ImageUrl = category.ImageUrl;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return;

            bool hasProducts = await _context.Products
                .AnyAsync(p => p.CategoryId == id);

            if (hasProducts)
                throw new ArgumentException(
                    "Cannot delete this category because it contains products.");

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}