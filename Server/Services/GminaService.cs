using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServiceGilBT.Data;

namespace WebServiceGilBT.Services {
    public class GminaService {

        readonly SqlDataAccess _db;

        public GminaService(SqlDataAccess db) {
            _db = db;
        }

        public async Task DeleteGminaAsync(Gmina argS) {
            string sql = @" DELETE FROM gminy
                            WHERE Id = @Id ";
            await _db.SaveDataAsync(sql, argS);
        }

        public async Task<List<Gmina>> GetGminaListAsync() {
            string sql = "select * from gminy";
            return await _db.LoadData<Gmina, dynamic>(sql, new { });
        }

        public async Task<Gmina> GetGminaAsync(int uid) {
            string sql = "select * from gminy where Id=" + uid;
            List<Gmina> listaPrzejsciowa = await _db.LoadData<Gmina, dynamic>(sql, new { });
            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    return listaPrzejsciowa[0];
                }
            }
            return null;
        }

        public async Task PostGminaAsync(Gmina argS) {
            string sql = @"insert into gminy (NazwaGminy, NazwaPowiatu, NazwaWojewodztwa)
                           values (@NazwaGminy, @NazwaPowiatu, @NazwaWojewodztwa);";
            await _db.SaveDataAsync(sql, argS);
        }

        public async Task UpdateGminaAsync(Gmina argS) {
            string sql = @" UPDATE gminy
                        SET NazwaGminy = @NazwaGminy , NazwaPowiatu=@NazwaPowiatu, NazwaWojewodztwa=@NazwaWojewodztwa
                        WHERE Id = @Id ";
            await _db.SaveDataAsync(sql, argS);
        }

    }
}
