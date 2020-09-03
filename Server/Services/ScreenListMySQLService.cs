using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Services {

    class ScreenInDB {
        public int uid { set; get; }
        public string name { set; get; }
        public string firmware_ver { set; get; }
        /* public byte[] firmware_bin { set; get; } */
        public int contrast { set; get; }
        public int contrast_night { set; get; }
        public int contrast_max { set; get; } = 4;
        public string last_request { set; get; }
        public eScreenType screen_type { set; get; }
        public bool from_led_screen { set; get; }
        public int width { set; get; }
        public int height { set; get; }
        public bool dhcp { set; get; }
        public string ip { set; get; }
        public string ma { set; get; }
        public string gw { set; get; }

        public byte[] pres { get; set; }

        public ScreenInDB() { }

        public ScreenInDB(Screen s) {
            uid = s.uid;
            name = s.name;
            firmware_ver = s.firmware_ver;
            contrast = s.contrast;
            contrast_max = s.contrast_max;
            contrast_night = s.contrast_night;
            last_request = s.last_request.ToString("yyyy-MM-dd HH:mm:ss");
            screen_type = s.screen_type;
            from_led_screen = s.from_led_screen;
            width = s.width;
            height = s.height;
            dhcp = s.dhcp;
            ip = s.ip;
            ma = s.ma;
            gw = s.gw;
            pres = objectToByteArray(s.pres);
        }
        public Screen oddajScreen() {
            Screen s = new Screen();

            s.uid = uid;
            s.name = name;
            s.firmware_ver = firmware_ver;
            s.contrast = contrast;
            s.contrast_max = contrast_max;
            s.contrast_night = contrast_night;

            s.last_request = DateTime.ParseExact(last_request, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            s.screen_type = screen_type;
            s.from_led_screen = from_led_screen;
            s.width = width;
            s.height = height;
            s.dhcp = dhcp;
            s.ip = ip;
            s.ma = ma;
            s.gw = gw;
            s.pres = ByteArrayToObject(pres);

            return s;
        }



        byte[] objectToByteArray(object obj) {
            if (obj != null) {
                BinaryFormatter binform = new BinaryFormatter();
                MemoryStream fs = new MemoryStream();
                binform.Serialize(fs, obj);
                fs.Close();
                return fs.ToArray();
            }
            return null;
        }

        Pres ByteArrayToObject(byte[] arrBytes) {
            if (arrBytes != null) {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return (Pres)obj;
            }
            return null;
        }


    }


    public class ScreenListMySQLService : IScreenListService {

        readonly SqlDataAccess _db;
        public ScreenListMySQLService(SqlDataAccess db) {
            _db = db;
        }

        public Task DeleteScreenAsync(Screen argS) {
            string sql = @" DELETE FROM screens
                            WHERE Id = @Id ";


            return _db.SaveData(sql, new ScreenInDB(argS));
        }

        public ScreenList GetGilBTScreenList() {
            return GetGilBTScreenListAsync().Result;
        }

        public Task<ScreenList> GetGilBTScreenListAsync() {
            string sql = "select * from screens";

            List<ScreenInDB> listaPrzejsciowa = _db.LoadData<ScreenInDB, dynamic>(sql, new { }).Result;

            ScreenList sl = new ScreenList();
            sl.Screens = new List<Screen>();
            foreach (ScreenInDB sidb in listaPrzejsciowa) {
                sl.Screens.Add(sidb.oddajScreen());
            }

            return Task.FromResult(sl);
        }

        public Task<Screen> GetGilBTSpecificScreenAsync(int uid) {
            string sql = "select * from screens where uid=" + uid;

            List<ScreenInDB> listaPrzejsciowa = _db.LoadData<ScreenInDB, dynamic>(sql, new { }).Result;

            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    Screen scr = listaPrzejsciowa[0].oddajScreen();
                    return Task.FromResult(scr);
                }
            }
            return null;
        }

        public Task PostScreenAsync(Screen argS) {
            string sql = @"insert into screens (uid , name,  firmware_ver , contrast, contrast_max, contrast_night, last_request, screen_type, from_led_screen, width, height, dhcp, ip, ma, gw, pres)
                           values (@uid, @name,  @firmware_ver , @contrast, @contrast_max, @contrast_night, @last_request, @screen_type, @from_led_screen, @width, @height, @dhcp, @ip, @ma, @gw, @pres);";


            return _db.SaveData(sql, new ScreenInDB(argS));
        }

        public Task UpdateScreenAsync(Screen argS) {

            string sql = @" UPDATE screens
                        SET name = @name,  firmware_ver = @firmware_ver , contrast = @contrast, contrast_max = @contrast_max, contrast_night = @contrast_night, last_request = @last_request, screen_type = @screen_type, from_led_screen = @from_led_screen, width = @width, height = @height, dhcp = @dhcp, ip = @ip, ma = @ma, gw = @gw, pres=@pres
                        WHERE uid = @uid ";


            return _db.SaveData(sql, new ScreenInDB(argS));
        }
    }
}
