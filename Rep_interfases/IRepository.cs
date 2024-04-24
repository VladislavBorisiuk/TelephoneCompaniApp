using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rep_interfases
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public async Task<T> GetByIdAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public async Task<T> AddAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public async Task<T> UpdateAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public async Task<T> DeleteAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
    }
}
