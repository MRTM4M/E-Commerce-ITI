using E_commerce_iti.Data;
using E_commerce_iti.Models;
using E_commerce_iti.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_iti.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceDB _context;

        public ProductService(ECommerceDB context)
        {
            _context = context;
        }

       
        private static ProductViewModel MapToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                Stock = product.Stock,
                CategoryName = product.Category.Name!
            };
        }

        
        private static Product MapToProduct(ProductViewModel model)
        {
            return new Product
            {
                Id = model.Id,
                CategoryId = model.CategoryId,
                Name = model.Name,
                Price = model.Price,
                Image = model.Image,
                Description = model.Description,
                Stock = model.Stock
            };
        }

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return products
                .Select(MapToProductViewModel)
                .ToList();
        }

        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            return MapToProductViewModel(product);
        }

        public async Task CreateAsync(ProductViewModel model)
        {
            if (model.Price <= 0)
                throw new ArgumentException(
                    "Product price must be greater than zero.");

            if (model.Stock < 0)
                throw new ArgumentException(
                    "Product stock cannot be negative.");

            bool productExists = await _context.Products
                .AnyAsync(p =>
                    p.Name == model.Name &&
                    p.CategoryId == model.CategoryId);

            if (productExists)
                throw new ArgumentException(
                    "This product already exists in this category.");

            var product = MapToProduct(model);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductViewModel model)
        {
            if (model.Price <= 0)
                throw new ArgumentException(
                    "Product price must be greater than zero.");

            if (model.Stock < 0)
                throw new ArgumentException(
                    "Product stock cannot be negative.");

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
                throw new ArgumentException("Product not found.");

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == model.CategoryId);

            if (category == null)
                throw new ArgumentException("Category not found.");

            product.CategoryId = category.Id;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Image = model.Image;
            product.Description = model.Description;
            product.Stock = model.Stock;
            product.Category = category;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductViewModel>> SearchAsync(string name)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            return products
                .Select(MapToProductViewModel)
                .ToList();
        }

        public async Task<List<ProductViewModel>> FilterAsync(string category)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == category)
                .ToListAsync();

            return products
                .Select(MapToProductViewModel)
                .ToList();
        }

        public async Task<List<ProductViewModel>> GetPagedAsync(
            int page,
            int pageSize)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products
                .Select(MapToProductViewModel)
                .ToList();
        }


        public async Task<ShopViewModel> GetShopAsync(
        string? search,
        int? categoryId,
        int page,
        int pageSize)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search));
            }

            // Category filter
            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            // Total products after search/filter
            var totalProducts = await query.CountAsync();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling(
                totalProducts / (double)pageSize);

            // Prevent invalid page numbers
            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            // Pagination
            var products = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Product → ProductViewModel
            var productViewModels = products
                .Select(MapToProductViewModel)
                .ToList();

            // Get categories
            var categories = await _context.Categories
                .ToListAsync();

            // Category → CategoryViewModel
            var categoryViewModels = categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl
                })
                .ToList();

            return new ShopViewModel
            {
                Products = productViewModels,
                Categories = categoryViewModels,

                Search = search,
                CategoryId = categoryId,

                CurrentPage = page,
                TotalPages = totalPages
            };
        }
    }
}