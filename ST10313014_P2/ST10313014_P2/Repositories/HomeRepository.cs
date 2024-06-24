
using Microsoft.EntityFrameworkCore;

namespace ST10313014_P2.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeRepository(ApplicationDbContext dbContext)
        {
                this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Category>> Categories()
        {
            return await _dbContext.Categories.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm="", int CategoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _dbContext.Products
                                                    join category in _dbContext.Categories
                            on product.CategoryId equals category.Id
                            where string.IsNullOrWhiteSpace(sTerm) || (product !=null && product.ProductName.ToLower().StartsWith(sTerm))
                            select new Product
                            {
                                ProductID = product.ProductID,
                                ProductImage = product.ProductImage,
                                ArtistName = product.ArtistName,
                                ProductName = product.ProductName,
                                Price = product.Price,
                                CategoryId = product.CategoryId,
                                Category = product.Category
                            }
                            ).ToListAsync();
            if (CategoryId > 0 )
            {
                products = products.Where(a => a.CategoryId == CategoryId).ToList();
            }
            return products;
        }
    }
}
