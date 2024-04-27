using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rep_interfases
{
    public interface INamedRepository<T> where T : class, IEntity, new()
    {
        public Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public Task<T> GetByIdAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public Task<T> GetByNameAsync(string name, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public Task<T> AddAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public Task UpdateAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public Task DeleteAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
    }
}
