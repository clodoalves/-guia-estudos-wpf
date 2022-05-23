using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFSample.Repository.Context;

namespace WPFSample.Repository.Contract
{
    public interface IRepositoryBase <TEntity> where TEntity:class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
