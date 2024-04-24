using Dapper;
using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Repositories
{
    internal class NamedEntityRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly IDbConnection _connection;

        public NamedEntityRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _connection.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(T entity)
        {
            string columns = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
            string parameters = string.Join(", ", typeof(T).GetProperties().Select(p => "@" + p.Name));

            string sql = $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task UpdateAsync(T entity)
        {
            string columns = string.Join(", ", typeof(T).GetProperties().Select(p => $"{p.Name} = @{p.Name}"));

            string sql = $"UPDATE {typeof(T).Name} SET {columns} WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(int id)
        {
            string sql = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
