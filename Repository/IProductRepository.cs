using ProductManagementWebAPI.Models;

namespace ProductManagementWebAPI.Repository
{
    public interface IProductRepository
    {
        Task<PagedResponse<ProductModel>> GetAllProductAsync(ProductQueryParameters query);
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task<int> AddProuctAsync(ProductModel model);
        Task<ProductModel?> UpdateProductAsync(int productId, ProductModel model);
        Task<bool> DeleteProductAsync(int productId);
    }
}