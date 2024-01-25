using exercise.wwwapi.Models;

namespace exercise.wwwapi.Repository
{
    public interface IRepository<T>
    {
        T Insert(T product);
        IEnumerable<T> Get();
        T Update(T entity);
        T Delete(int id);
        T GetById(object id);
        void Save();
    }
}
