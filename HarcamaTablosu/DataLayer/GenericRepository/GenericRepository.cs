using DataLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.GenericRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly Context db;
        //Context db = new Context();
        DbSet<T> _obj;

        public GenericRepository(Context db)
        {
            _obj = db.Set<T>();
            this.db = db;
        }

        public void Delete(T entity)
        {
            var deletedEntity = db.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            db.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _obj.SingleOrDefault(filter);
        }

        public void Insert(T entity)
        {
            var insertedEntity = db.Entry(entity);
            insertedEntity.State = EntityState.Added;
            db.SaveChanges();
        }

        public List<T> List()
        {
            return _obj.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _obj.Where(filter).ToList();
        }

        public void Update(T entity)
        {
            var updatedEntity = db.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
