using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data;
using Entities;
using Interfaces;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private DB_Context _context;

        public DB_Context Context { get => _context; }

        public Repository(DB_Context context)
        {
            this._context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        virtual public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public int Insert(T g)
        {
            Context.Set<T>().Add(g);
            return Context.SaveChanges();
        }

        public int Update(T g)
        {
            Context.Entry<T>(g).State = System.Data.Entity.EntityState.Modified;
            return Context.SaveChanges();
        }

        public int Delete(T g)
        {
            Context.Set<T>().Remove(g);
            return Context.SaveChanges();
        }
    }
}
