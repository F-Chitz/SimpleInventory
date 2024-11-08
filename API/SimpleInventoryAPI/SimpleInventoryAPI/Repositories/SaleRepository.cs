using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Repositories
{
    public class SaleRepository : IBaseRespository<InventoryMove>
    {
        private readonly SimpleInventoryDbContext _context;

        public SaleRepository(SimpleInventoryDbContext context)
        {
            _context = context;
        }

        public bool Add(InventoryMove Entity)
        {
            Entity.MoveType = Data.InventoryMoveType.Sale;
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
                    .Where(e => e.MoveType == Data.InventoryMoveType.Sale)
                    .Include(e => e.People)
                    .Include(e => e.Items)
                    .ThenInclude(i => i.Product)
                    .ToListAsync();
            }
            else
            {
                return await _context.inventoryMoves
                    .Where(e => e.IsActive == true && 
                                e.MoveType == Data.InventoryMoveType.Sale)
                    .Include(e => e.People)
                    .Include(e => e.Items)
                    .ThenInclude(i => i.Product)
                    .ToListAsync();
            }
        }

        public async Task<InventoryMove> GetById(int Id)
        {
            return await _context.inventoryMoves
                .Where(e => e.Id == Id &&
                            e.MoveType == Data.InventoryMoveType.Sale)
                .Include(e => e.People)
                .Include(e => e.Items)
                .ThenInclude(i => i.Product)
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
