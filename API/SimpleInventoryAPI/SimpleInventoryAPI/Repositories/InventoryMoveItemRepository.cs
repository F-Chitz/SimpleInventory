using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Repositories
{
    public class InventoryMoveItemRepository : IBaseRespository<InventoryMoveItem>
    {
        private readonly SimpleInventoryDbContext _context;

        public InventoryMoveItemRepository(SimpleInventoryDbContext context)
        {
            _context = context;
        }

        public bool Add(InventoryMoveItem Entity)
        {
            _context.Attach(Entity);
            return Save();
        }

        public bool Delete(InventoryMoveItem Entity)
        {
            Entity.DeleteAt = DateTime.Now;
            Entity.IsActive = false;
            _context.Update(Entity);
            return Save();
        }

        public async Task<IEnumerable<InventoryMoveItem>> GetAll(bool IncludeInactives = false)
        {
            if (IncludeInactives)
            {
                return await _context.inventoryMoveItems
                             .Include(e => e.Product)
                             .ToListAsync();
            }
            else
            {
                return await _context.inventoryMoveItems
                             .Include(e => e.Product)
                             .ToListAsync();
            }
        }

        public async Task<InventoryMoveItem> GetById(int Id)
        {
            return await _context.inventoryMoveItems
                         .Include(e => e.Product)
                         .FirstOrDefaultAsync(e => e.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(InventoryMoveItem Entity)
        {
            Entity.UpdateAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }
    }
}
