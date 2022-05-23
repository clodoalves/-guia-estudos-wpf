using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Repository.Context;
using WPFSample.Repository.Contract;

namespace WPFSample.Repository.Implementation
{
    public class FakeRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private ObservableCollection<TEntity> _data;
        private IQueryable _query;
        public IWPFSampleDbContext Db => new FakeWPFSampleDbContext();

        public FakeRepositoryBase()
        {
            _data = new ObservableCollection<TEntity>();
            _query = _data.AsQueryable();
        }

        public TEntity Add(TEntity obj)
        {
            _data.Add(obj);
            return obj;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _data;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
