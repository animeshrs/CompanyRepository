using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task CreateAsync(T type);

        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
    }
}
