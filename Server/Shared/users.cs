using WebServiceGilBT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace WebServiceGilBT.Shared {
    public class UserList {

        private static List<User> _users = null;

        public static List<User> users {
            get {
                if (_users == null) {
                    Load();
                }
                return _users;
            }
        }

        public static void Add(User argUser) {
            if (argUser != null) {
                foreach (User u in users) {
                    if (u.EmailAddress == argUser.EmailAddress) {
                        Console.WriteLine("users.cs: user already exists, cant add");
                        return;
                    }
                }
                _users.Add(argUser);
                Save();
            }
        }

        //users file name
        private const string ufn = "db/users.json";

        private static object locker = new object();

        public static void Save() {
            lock (locker) {
                if (_users == null) {
                    _users = new List<User>();
                }
                //encrypt
                foreach (User u in _users) {
                    u.Password = EncryptString.StringCipher.Encrypt(u.Password);
					if ( u.UserId==0 ){
						Random r = new Random();
						u.UserId = r.Next();
					}
                }
                //saving process
                try {
                    try {
                        DateTime now = MyClock.Now;
                        File.Copy(ufn, $"{ufn}.{now.ToString()}.back");
                    } catch {
                    }
                    string serialised_users = JsonSerializer.Serialize(_users);
                    File.WriteAllText(ufn, serialised_users);
                } catch {
                    Console.WriteLine("some exception during saving");
                }
                //decrypt
                foreach (User u in _users) {
                    u.Password = EncryptString.StringCipher.Decrypt(u.Password);
                }
            }
        }

        private static void Load() {
            lock (locker) {
                try {
                    String serialised_users = File.ReadAllText(ufn);
                    _users = JsonSerializer.Deserialize<List<User>>(serialised_users);
                    foreach (User u in _users) {
                        u.Password = EncryptString.StringCipher.Decrypt(u.Password);
                    }
                } catch {
                    Debuger.PrintLn("Reading {0} failed.", ufn);
                }
            }
        }
    }
}
