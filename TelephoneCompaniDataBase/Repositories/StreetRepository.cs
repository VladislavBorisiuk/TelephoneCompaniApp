﻿using Dapper;
using Rep_interfases;
using System.Data;
using TelephoneCompaniDataBase.Entityes;

namespace TelephoneCompaniDataBase.Repositories
{
    public class StreetRepository : IRepository<Street>
    {
        private readonly IDbConnection _connection;

        public StreetRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Street>> GetAllAsync()
        {
            return await _connection.QueryAsync<Street>("SELECT * FROM Streets");
        }

        public async Task<Street> GetByIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Street>("SELECT * FROM Streets WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(Street street)
        {
            if (street is null) throw new ArgumentNullException(nameof(street));
            string columns = "StreetName";
            string parameters = "@StreetName";

            string sql = $"INSERT INTO Streets ({columns}) VALUES ({parameters})";
            await _connection.ExecuteAsync(sql, street);
        }

        public async Task UpdateAsync(Street street)
        {
            if (street is null) throw new ArgumentNullException(nameof(street));
            string sql = "UPDATE Streets SET StreetName = @StreetName WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, street);
        }

        public async Task DeleteAsync(int id)
        {
            string sql = "DELETE FROM Streets WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}