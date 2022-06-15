using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public class RepositoryBase<T> where T : class, IRepositoryBase<T>
    {
        protected WPFSampleDbContext dbContext;
        public void Add(T register)
        {
            dbContext.Add(register);
            dbContext.SaveChanges();
        }

        public void Delete(T register)
        {
            dbContext.Remove(register);
            dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public void Update(T register)
        {
            dbContext.Update(register);
            dbContext.SaveChanges();
        }
    }
}
