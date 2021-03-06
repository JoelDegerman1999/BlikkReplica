﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlikkBasicReplica.API.Repositories
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
