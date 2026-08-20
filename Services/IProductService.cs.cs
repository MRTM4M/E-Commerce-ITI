using E_commerce_iti.ViewModels;

namespace E_commerce_iti.Services
{
    public interface IProductService
    {
        Task<ShopViewModel> GetShopAsync(
            string? search,
            int? categoryId,
            int page,
            int pageSize);

        Task<List<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel?> GetByIdAsync(int id);

        Task CreateAsync(ProductViewModel model);

        Task UpdateAsync(ProductViewModel model);

        Task DeleteAsync(int id);

        Task<List<ProductViewModel>> SearchAsync(string name);

        Task<List<ProductViewModel>> FilterAsync(string category);

        Task<List<ProductViewModel>> GetPagedAsync(int page, int pageSize);
    }
}