using Inlamningsuppgift.Entities;
using Inlamningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Inlamningsuppgift.Services
{
    public interface IProductService
    {
        Task<ProductCreatedModel> CreateAsync(CreateProductModel product);
        Task<IEnumerable<ReadProductModel>> GetAllAsync();
        Task<ReadProductModel> GetAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<ReadProductModel> UpdateAsync(int id, UpdateProductModel model);
    }
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProductCreatedModel> CreateAsync(CreateProductModel model)
        {
            

            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName);

            if (category == null)
            {
                category = new Category
                {
                    CategoryName = model.CategoryName,
                };
                _dataContext.Categories.Add(category);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                category.CategoryName = model.CategoryName;
            }

            if (!await _dataContext.Products.AnyAsync(x => x.Name == model.Name))
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = (await _dataContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId)).CategoryId
                };

                _dataContext.Products.Add(product);
                await _dataContext.SaveChangesAsync();

                return new ProductCreatedModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId
                };
            }
            return null!;
        }

        

        public async Task<IEnumerable<ReadProductModel>> GetAllAsync()
        {
            var allProducts = new List<ReadProductModel>();

            foreach (Product product in await _dataContext.Products.Include(x => x.Categories).ToListAsync())
            {
                allProducts.Add(new ReadProductModel()
                {
                    ProductId = product.ProductId,
                    CategoryName = product.Categories.CategoryName,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                });
            }
            return allProducts;
        }

        public async Task<ReadProductModel> GetAsync(int id)
        {
            var product = await _dataContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.ProductId == id);

            if (product != null)
            {
                return new ReadProductModel
                {
                    ProductId = product.ProductId,
                    CategoryName = product.Categories.CategoryName,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price

                };
            } 
            return null!;
        }


        public async Task<ReadProductModel> UpdateAsync(int id, UpdateProductModel model)
        {
            var product = await _dataContext.Products.Include(x => x.Categories).FirstOrDefaultAsync(x => x.ProductId == id);
            if (product != null)
            {
                if (product.Name != model.Name && !string.IsNullOrEmpty(model.Name))
                {
                    product.Name = model.Name;
                }

                if (product.Description != model.Description && !string.IsNullOrEmpty(model.Description))
                {
                    product.Description = model.Description;
                }

                if (product.Price != model.Price )
                {
                    product.Price = model.Price;
                }

                _dataContext.Entry(product).State = EntityState.Modified;
                await _dataContext.SaveChangesAsync();

                return new ReadProductModel
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryName = product.Categories.CategoryName,
                };
            }

            return null!;
        }






        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _dataContext.Products.FindAsync(id);
            if (product != null)
            {
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            return false;

        }

      
    }
}


