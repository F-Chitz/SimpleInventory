
namespace SimpleInventoryAPI.Interfaces
{
    public interface IBaseRespository<T>
    {
        public Task<IEnumerable<T>> GetAll(bool IncludeInactives = false);

        public Task<T> GetById(int Id);

        public bool Add(T Entity);

        public bool Update(T Entity);

        public bool Delete(T Entity);

        public bool Save();
    }
}
