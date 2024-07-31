using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        T? Get(int id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
