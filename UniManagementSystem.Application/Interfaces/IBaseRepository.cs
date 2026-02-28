using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Infrastructure.Repositories
{
    internal interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<T> GetStringId(string id);
        Task<T> GetByName (string name);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
