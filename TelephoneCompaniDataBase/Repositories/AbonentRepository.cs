using Dapper;
using Rep_interfases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneCompaniDataBase.Entityes;
using TelephoneCompaniDataBase.Entityes.Base;

namespace TelephoneCompaniDataBase.Repositories
{
    public class AbonentRepository : IRepository<Abonent> 
    {
        private readonly IDbConnection _connection;

        public AbonentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Abonent>> GetAllAsync()
        {
            return await _connection.QueryAsync<Abonent>("SELECT * FROM Abonent");
        }

        public async Task<Abonent> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Abonent>("SELECT * FROM Abonent WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(Abonent abonent)
        {
            if (abonent is null) throw new ArgumentNullException(nameof(abonent));
            string columns = "FullName";
            string parameters = "@FullName";

            string sql = $"INSERT INTO Abonent ({columns}) VALUES ({parameters})";
            await _connection.ExecuteAsync(sql, abonent);
        }

        public async Task UpdateAsync(Abonent abonent)
        {
            if (abonent is null) throw new ArgumentNullException(nameof(abonent));
            string sql = "UPDATE Abonent SET FullName = @FullName WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, abonent);
        }

        public async Task DeleteAsync(int id)
        {
            string sql = "DELETE FROM Abonent WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
