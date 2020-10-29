using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace WebServiceGilBT.Services {
    public class UniversalMysqlService<T> {
        readonly SqlDataAccess _db;

        string tabelaName;

        string IdName;

        List<string> propertyNames;

        public UniversalMysqlService(SqlDataAccess db, string tableName, string PrimaryKeyPropertyName) {
            _db = db;
            this.tabelaName = tableName;
            IdName = PrimaryKeyPropertyName;
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            propertyNames = new List<string>();
            foreach (PropertyInfo pi in propertyInfos) {
                if (pi.Name != IdName) {
                    propertyNames.Add(pi.Name);
                }
            }
        }

        public async Task DeleteRecordAsync(T argS) {
            string sql = string.Format(@" DELETE FROM {0}
                            WHERE {1} = @{1} ", tabelaName, IdName);
            await _db.SaveDataAsync(sql, argS);
        }

        public async Task<List<T>> GetAllRecordsAsync() {
            string sql = "select * from " + tabelaName;
            return await _db.LoadData<T, dynamic>(sql, new { });
        }

        public async Task<T> GetRecordByIdAsync(int id) {
            string sql = string.Format("select * from {0} where {1}={2}", tabelaName, IdName, id);
            List<T> listaPrzejsciowa = await _db.LoadData<T, dynamic>(sql, new { });
            if (listaPrzejsciowa != null) if (listaPrzejsciowa.Count > 0)
                    return listaPrzejsciowa[0];
            return default;
        }

        public async Task PostRecordAsync(T argS) {
            string names = "";
            string values = "";
            foreach (string s in propertyNames) {
                names += s + " , ";
                values += $"@{s} , ";
            }
            names = names.Substring(0, names.Length - 2);
            values = values.Substring(0, values.Length - 2);
            string sql = string.Format(@"insert into {0} ({1})
                           values ({2});", tabelaName, names, values);
            await _db.SaveDataAsync(sql, argS);
        }

        public async Task UpdateRecordAsync(T argS, int id) {
            string sets = "";
            foreach (string s in propertyNames) {
                sets += $"{s} = @{s} , ";
            }
            sets = sets.Substring(0, sets.Length - 2);
            string sql = string.Format(@" UPDATE {0}
                        SET {1}
                        WHERE {2} = {3} ", tabelaName, sets, IdName, id);
            await _db.SaveDataAsync(sql, argS);
        }

        public async Task UpdateCertainProperiesInRecordAsync(T argS, List<string> listOfPropertyNames, int id) {
            string sets = "";
            foreach (string s in listOfPropertyNames) {
                sets += $"{s} = @{s} , ";
            }
            sets = sets.Substring(0, sets.Length - 2);

            string sql = string.Format(@" UPDATE {0}
                        SET {1}
                        WHERE {2} = {3} ", tabelaName, sets, IdName, id);
            await _db.SaveDataAsync(sql, argS);
        }

    }
}
