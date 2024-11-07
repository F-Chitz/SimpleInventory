using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Repositories
{
    public class InventoryMoveRepository : IBaseRespository<InventoryMove>
    {
        private readonly SimpleInventoryDbContext _context;

        public InventoryMoveRepository(SimpleInventoryDbContext context)
        {
            _context = context;
        }

        public bool Add(InventoryMove Entity)
        {
            _context.Attach(Entity);
            return Save();
        }

        public bool Delete(InventoryMove Entity)
        {
            Entity.IsActive = false;
            Entity.DeleteAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }

        public async Task<IEnumerable<InventoryMove>> GetAll(bool IncludeInactives = false)
        {
            if (IncludeInactives)
            {
                return await _context.inventoryMoves
                    .Include(e => e.People)
                    .Include(e => e.Items)
                    .ToListAsync();
            }
            else
            {
                return await _context.inventoryMoves
                    .Where(e => e.IsActive == true)
                    .Include(e => e.People)
                    .Include(e => e.Items)
                    .ToListAsync();
            }
        }

        public async Task<InventoryMove> GetById(int Id)
        {
            return await _context.inventoryMoves
                .Where(e => e.Id == Id)
                .Include(e => e.People)
                .Include(e => e.Items)
                .FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(InventoryMove Entity)
        {
            Entity.UpdateAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }
    }
}
