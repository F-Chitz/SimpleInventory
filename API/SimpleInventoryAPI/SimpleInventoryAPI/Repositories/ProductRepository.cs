using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Repositories
{
    public class ProductRepository : IBaseRespository<Product>
    {
        private readonly SimpleInventoryDbContext _context;

        public ProductRepository(SimpleInventoryDbContext context)
        {
            _context = context;
        }

        public bool Add(Product Entity)
        {
            _context.Add(Entity);
            return Save();
        }

        public bool Delete(Product Entity)
        {
            Entity.IsActive = false;
            Entity.DeleteAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll(bool IncludeInactives = false)
        {
            if (IncludeInactives)
            {
                return await _context.Products.ToListAsync();
            }
            else
            {
                return await _context.Products.Where(p => p.IsActive == true).ToListAsync();
            }
        }

        public async Task<Product> GetById(int Id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Product Entity)
        {
            Entity.UpdateAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }
    }
}
