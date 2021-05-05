using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T type)
        {
            await _context.Set<T>().AddAsync(type);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }
    }
}
