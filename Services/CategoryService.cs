using E_commerce_iti.Data;
using E_Commerce_iti.Models;

namespace E_Commerce_iti.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceDBcontext _context;

        public CategoryService(ECommerceDBcontext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

       
        public Category GetById(int id)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Id == id);
        }

   
        public void Create(Category category)
        {
            bool categoryExists = _context.Categories
                .Any(c => c.Name.ToLower() == category.Name.ToLower());

            if (categoryExists)
                throw new ArgumentException("Category already exists.");

            _context.Categories.Add(category);
            _context.SaveChanges();
        }

      
        public void Update(Category category)
        {
            var existingCategory = _context.Categories
                .FirstOrDefault(c => c.Id == category.Id);

            if (existingCategory == null)
                return;

            bool categoryExists = _context.Categories
                .Any(c => c.Id != category.Id &&
                         c.Name.ToLower() == category.Name.ToLower());

            if (categoryExists)
                throw new ArgumentException("Category already exists.");

            existingCategory.Name = category.Name;

            _context.SaveChanges();
        }

       
        public void Delete(int id)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
                return;

            bool hasProducts = _context.Products
                .Any(p => p.CategoryId == id);

            if (hasProducts)
                throw new ArgumentException(
                    "Cannot delete this category because it contains products.");

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}