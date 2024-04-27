using Dapper;
using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniDataBase.Repositories
{
    public class AbonentRepository : INamedRepository<Abonent>
    {
        private readonly IDbConnection _connection;

        public AbonentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Abonent>> GetAllAsync(CancellationToken Cancel = default)
        {
            return await _connection.QueryAsync<Abonent>("SELECT * FROM Abonent");
        }

        public async Task<Abonent> GetByIdAsync(int id, CancellationToken Cancel = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<Abonent>("SELECT * FROM Abonent WHERE Id = @Id", new { Id = id });
        }

        public async Task<Abonent> GetByNameAsync(string name, CancellationToken Cancel = default)
        {
            return await _connection.QueryFirstOrDefaultAsync<Abonent>("SELECT * FROM Abonent WHERE FullName = @Name", new { Name = name });
        }

        public async Task<Abonent> AddAsync(Abonent abonent, CancellationToken Cancel = default)
        {
            if (abonent is null) throw new ArgumentNullException(nameof(abonent));
            string columns = "FullName";
            string parameters = "@FullName";

            string sql = $"INSERT INTO Abonent ({columns}) VALUES ({parameters}); SELECT SCOPE_IDENTITY();";
            var id = await _connection.ExecuteScalarAsync<int>(sql, abonent);
            abonent.Id = id;
            return abonent;
        }

        public async Task UpdateAsync(Abonent abonent, CancellationToken Cancel = default)
        {
            if (abonent is null) throw new ArgumentNullException(nameof(abonent));
            string sql = "UPDATE Abonent SET FullName = @FullName WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, abonent);
        }

        public async Task DeleteAsync(int id, CancellationToken Cancel = default)
        {
            try
            {
                string sql = "DELETE FROM Abonent WHERE Id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}

