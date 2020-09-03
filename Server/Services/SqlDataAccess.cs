using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceGilBT.Services {
    public class SqlDataAccess {
        readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Nazwa";
        public SqlDataAccess(IConfiguration config) {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters) {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }


        public async Task SaveData<T>(string sql, T parameters) {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task UpdataValue(string sql) {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
