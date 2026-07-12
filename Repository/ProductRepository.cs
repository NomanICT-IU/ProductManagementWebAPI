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


        public async Task<PagedResponse<ProductModel>> GetAllProductAsync(ProductQueryParameters query)
        {
            IQueryable<Product> products = _context.Products.AsNoTracking();


            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                string search = query.Search.ToLower();

                products = products.Where(x =>
                    x.Name!.ToLower().Contains(search) ||
                    x.Description!.ToLower().Contains(search));
            }


            products = (query.SortBy.ToLower(), query.SortOrder.ToLower()) switch
            {
                ("name", "desc") => products.OrderByDescending(x => x.Name),
                ("name", _) => products.OrderBy(x => x.Name),

                ("price", "desc") => products.OrderByDescending(x => x.Price),
                ("price", _) => products.OrderBy(x => x.Price),

                ("quantity", "desc") => products.OrderByDescending(x => x.Quantity),
                ("quantity", _) => products.OrderBy(x => x.Quantity),

                ("id", "desc") => products.OrderByDescending(x => x.Id),

                _ => products.OrderBy(x => x.Id)
            };

            var totalRecords = await products.CountAsync();

            var records = await products
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResponse<ProductModel>
            {
                Data = _mapper.Map<List<ProductModel>>(records),
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
                TotalRecords = totalRecords
            };
        }



        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            var result = await _context.Products.FindAsync(id);
            if (result == null)
                return null;

            return _mapper.Map<ProductModel>(result);

        }


        public async Task<int> AddProuctAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync();

            return newProduct.Id;
        }


        public async Task<ProductModel?> UpdateProductAsync(int productId, ProductModel model)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return null;

            _mapper.Map(model, product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductModel>(product);
        }


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
