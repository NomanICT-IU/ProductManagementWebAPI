using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementWebAPI.Data;
using ProductManagementWebAPI.Models;

namespace ProductManagementWebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        //get all Products
        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var records = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductModel>>(records);
        }

        //get product

        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
                return null;

            return _mapper.Map<ProductModel>(result);

        }

        //add Book
        public async Task<int> AddProuctAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync();

            return newProduct.Id;
        }

        //Update Product
        public async Task<ProductModel?> UpdateProductAsync(int productId, ProductModel model)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return null;

            _mapper.Map(model, product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductModel>(product);
        }

        //Delete Product
        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return false;

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
