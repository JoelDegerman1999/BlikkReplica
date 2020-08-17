using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlikkBaiscReplica.Repositories
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Update(T entity);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);
    }
}
