using System.Collections.Generic;
using System.Linq;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected WPFSampleDbContext dbContext = new WPFSampleDbContext();
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
            //TODO: To find a way to refresh the global dbContext correctly
            using (var dbContext = new WPFSampleDbContext())
            {
                return dbContext.Set<T>().ToList();
            }
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
