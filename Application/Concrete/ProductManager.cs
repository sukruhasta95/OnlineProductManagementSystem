using Application.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductManager(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task AddAsync(Product product)
        {
            await _repo.AddAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            return _repo.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
           await _repo.UpdateAsync(product);
        }
    }
}
