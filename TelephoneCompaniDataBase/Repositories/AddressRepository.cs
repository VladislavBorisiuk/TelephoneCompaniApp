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
    public class AddressRepository : IRepository<Address>
    {
        private readonly IDbConnection _connection;

        public AddressRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Address>> GetAllAsync(CancellationToken Cancel = default)
        {
            return await _connection.QueryAsync<Address>("SELECT * FROM Address");
        }

        public async Task<Address> GetByIdAsync(int id, CancellationToken Cancel = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<Address>("SELECT * FROM Address WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(Address entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            string columns = string.Join(", ", typeof(Address).GetProperties().Where(p => p.Name != "Id").Select(p => p.Name));
            string parameters = string.Join(", ", typeof(Address).GetProperties().Where(p => p.Name != "Id").Select(p => "@" + p.Name));

            string sql = $"INSERT INTO Address ({columns}) VALUES ({parameters})";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task UpdateAsync(Address entity, CancellationToken Cancel = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            string columns = string.Join(", ", typeof(Address).GetProperties().Where(p => p.Name != "Id").Select(p => $"{p.Name} = @{p.Name}"));

            string sql = $"UPDATE Address SET {columns} WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(int id, CancellationToken Cancel = default)
        {
            string sql = $"DELETE FROM Address WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task GetByAbonentIdAsync(int id, CancellationToken Cancel = default)
        {
            string sql = $"SELECT * FROM Address WHERE AbonentId = {id}";
            await _connection.ExecuteAsync(sql, new Address() );
        }

    }
}
