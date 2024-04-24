using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rep_interfases
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        public IQueryable<T> Items { get; }

        public T Get(int id);

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }

        public T Add(T item);

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }

        public void Update(T item);

        public async Task<T> UpdateAsync(T item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
        public void Remove(int id);
        public async Task<T> RemoveAsync(int id, CancellationToken Cancel = default) { throw new NotImplementedException(); }
    }
}
