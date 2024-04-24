using Dapper;
using Rep_interfases;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TelephoneCompaniDataBase.Entityes;
using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Repositories
{
    public class PhoneNumberRepository : IRepository<PhoneNumber>
    {
        private readonly IDbConnection _connection;

        public PhoneNumberRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<PhoneNumber>> GetAllAsync()
        {
            return await _connection.QueryAsync<PhoneNumber>("SELECT * FROM PhoneNumber");
        }

        public async Task<PhoneNumber> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<PhoneNumber>("SELECT * FROM PhoneNumber WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(PhoneNumber entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            string columns = string.Join(", ", typeof(PhoneNumber).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name));
            string parameters = string.Join(", ", typeof(PhoneNumber).GetProperties().Where(p => p.Name != "Id").Select(p => "@" + p.Name));

            string sql = $"INSERT INTO PhoneNumber ({columns}) VALUES ({parameters})";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task UpdateAsync(PhoneNumber entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            string columns = string.Join(", ", typeof(PhoneNumber).GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));

            string sql = $"UPDATE PhoneNumber SET {columns} WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(int id)
        {
            string sql = $"DELETE FROM PhoneNumber WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
