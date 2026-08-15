using E_Commerce_iti.Models;

namespace E_Commerce_iti.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
        List<Product> Search(string name);
        List<Product> Filter(string category);
        List<Product> GetPaged(int page, int pageSize);
    }
}