using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSample.Repository.Contract
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T register);

        void Delete(T register);

        void Update(T register);

        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}
