using E_commerce_iti.Data;
using E_Commerce_iti.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_iti.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceDBcontext _context;

        public ProductService(ECommerceDBcontext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            return _context.Products
                .Include(p => p.Category)
                .ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Create(Product product)
        {
            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            if (product.Quantity < 0)
                throw new ArgumentException("Product quantity cannot be negative.");

            bool productExists = _context.Products.Any(p =>
                p.Name == product.Name &&
                p.CategoryId == product.CategoryId);

            if (productExists)
                throw new ArgumentException("This product already exists in this category.");

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public List<Product> Search(string name)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(name))
                .ToList();
        }

        public List<Product> Filter(string category)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == category)
                .ToList();
        }

        public List<Product> GetPaged(int page, int pageSize)
        {
            return _context.Products
                .Include(p => p.Category)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}