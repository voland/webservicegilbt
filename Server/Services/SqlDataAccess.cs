using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceGilBT.Services {
    public class SqlDataAccess {
        readonly IConfiguration _config;

        public string connectionString {
            get { return WebServiceGilBT.Shared.AppSettings.appSettings.ConnectionStrings.Nazwa; }
        }

        public SqlDataAccess(IConfiguration config) {
			//actualy does notnig important now
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters) {
            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }


        public async Task SaveDataAsync<T>(string sql, T parameters) {
            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task UpdataValue(string sql) {
            using (IDbConnection connection = new MySqlConnection(connectionString)) {
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
