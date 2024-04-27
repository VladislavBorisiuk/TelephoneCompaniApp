using Dapper;
using Rep_interfases;
using System.Data;
using System.IO;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniDataBase.Repositories
{
    public class StreetRepository : INamedRepository<Street>
    {
        private readonly IDbConnection _connection;

        public StreetRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Street>> GetAllAsync(CancellationToken Cancel = default)
        {
            return await _connection.QueryAsync<Street>("SELECT * FROM Streets");
        }

        public async Task<Street> GetByIdAsync(int id, CancellationToken Cancel = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<Street>("SELECT * FROM Streets WHERE Id = @Id", new { Id = id });
        }

        public async Task<Street> GetByNameAsync(string name, CancellationToken Cancel = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<Street>("SELECT * FROM Streets WHERE StreetName = @Name", new { Name = name });
        }

        public async Task<Street> AddAsync(Street street, CancellationToken Cancel = default)
        {
            if (street is null) throw new ArgumentNullException(nameof(street));

            string sql = $"SELECT COUNT(*) FROM Streets WHERE StreetName = @StreetName";
            int count = await _connection.ExecuteScalarAsync<int>(sql, new { StreetName = street.StreetName });

            if (count > 0)
            {
                sql = $"UPDATE Streets SET NumberOfSubscribers = NumberOfSubscribers + 1 WHERE StreetName = @StreetName";
                await _connection.ExecuteAsync(sql, new { StreetName = street.StreetName });
                return null; // Возвращаем null, так как улица уже существует
            }

            string columns = "StreetName";
            string parameters = "@StreetName";

            sql = $"INSERT INTO Streets ({columns}) OUTPUT INSERTED.Id VALUES ({parameters})";
            var id = await _connection.ExecuteScalarAsync<int>(sql, street);
            street.Id = id;
            return street;
        }



        public async Task UpdateAsync(Street street, CancellationToken Cancel = default)
        {
            if (street is null) throw new ArgumentNullException(nameof(street));
            string sql = "UPDATE Streets SET StreetName = @StreetName WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, street);
        }

        public async Task DeleteAsync(int id, CancellationToken Cancel = default)
        {
            string sql = $"UPDATE Streets SET NumberOfSubscribers = NumberOfSubscribers - 1 WHERE Id = @id";
            await _connection.ExecuteAsync(sql, new { Id = id });
            var street = await _connection.QueryFirstOrDefaultAsync<Street>("SELECT * FROM Streets WHERE Id = @Id", new { Id = id });
            if(street.NumberOfSubscribers == 0)
            {
                sql = "DELETE FROM Streets WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                return;
            }
            return;
        }

    }
}
