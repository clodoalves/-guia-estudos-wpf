using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public WPFSampleDbContext Db => new WPFSampleDbContext();

        public TEntity Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();

            return obj;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task<IEnumerable<TEntity>>.Factory.StartNew(() =>
            {
                return Db.Set<TEntity>().AsParallel().ToList();
            });
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
            Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
