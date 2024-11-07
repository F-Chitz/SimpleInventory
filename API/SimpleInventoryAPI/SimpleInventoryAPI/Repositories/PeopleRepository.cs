using Microsoft.EntityFrameworkCore;
using SimpleInventoryAPI.Data;
using SimpleInventoryAPI.Interfaces;
using SimpleInventoryAPI.Models;

namespace SimpleInventoryAPI.Repositories
{
    public class PeopleRepository : IBaseRespository<People>
    {
        private readonly SimpleInventoryDbContext _context;

        public PeopleRepository(SimpleInventoryDbContext context)
        {
            _context = context;
        }
        public bool Add(People Entity)
        {
            _context.Add(Entity);
            return Save();
        }

        public bool Delete(People Entity)
        {
            Entity.IsActive = false;
            Entity.DeleteAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }

        public async Task<IEnumerable<People>> GetAll(bool IncludeInactives = false)
        {
            if (IncludeInactives)
            {
               return await _context.Peoples.ToListAsync();
            }
            else
            {
                return await _context.Peoples.Where(p => p.IsActive).ToListAsync();
            }
        }

        public async Task<People> GetById(int Id)
        {
            return await _context.Peoples.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(People Entity)
        {
            Entity.UpdateAt = DateTime.Now;
            _context.Update(Entity);
            return Save();
        }
    }
}
