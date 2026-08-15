using E_Commerce_iti.Models;

namespace E_Commerce_iti.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Create(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}