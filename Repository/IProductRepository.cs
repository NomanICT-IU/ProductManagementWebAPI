using ProductManagementWebAPI.Models;

namespace ProductManagementWebAPI.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductModel>> GetAllProductAsync();
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<int> AddProuctAsync(ProductModel model);
        Task<ProductModel?> UpdateProductAsync(int productId, ProductModel model);
        Task<bool> DeleteProductAsync(int productId);
    }
}