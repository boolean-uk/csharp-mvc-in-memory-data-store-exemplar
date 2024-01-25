using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace exercise.wwwapi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
       private DataContext _db;
        private DbSet<T> _table = null;
        public Repository(DataContext dataContext)
        {
            _db = dataContext;
            _table = _db.Set<T>();
        }
       

        public T Delete(int id)
        {
            T entity = _table.Find(id);
            _table.Remove(entity);
            _db.SaveChanges();
            return entity;

        }

        public IEnumerable<T> Get()
        {
            return _table.ToList();
        }

       
        public T Insert(T entity)
        {
            _table.Add(entity);
            _db.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            _table.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity;
        }

        public T Delete(object id)
        {
            T entity = _table.Find(id);
            _table.Remove(entity);
            _db.SaveChanges();
            return entity;
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
