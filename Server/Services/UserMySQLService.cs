using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using WebServiceGilBT.Data;

namespace WebServiceGilBT.Services {

    class UserInDB {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public byte[] ScreenAccessList { set; get; }
        public string ConfirmPassword { get; set; }
        public string AdditionalInfo { get; set; }

        public UserInDB() { }

        public UserInDB(User u) {
            UserId = u.UserId;
            EmailAddress = u.EmailAddress;
            Password = EncryptString.StringCipher.Encrypt(u.Password);
            FirstName = u.FirstName;
            LastName = u.LastName;
            UserType = u.UserType.ToString();
            ConfirmPassword = u.ConfirmPassword;
            AdditionalInfo = u.AdditionalInfo;
            if (u.ScreenAccessList != null) {
                ScreenAccessList = objectToByteArray(u.ScreenAccessList);
            } else {
                ScreenAccessList = null;
            }
        }

        public User ToUser() {
            User u = new User();
            u.UserId = UserId;
            u.EmailAddress = EmailAddress;
            u.Password = EncryptString.StringCipher.Decrypt(Password);
			Console.WriteLine("decrypted password {0}, {1}", Password, u.Password);
            u.FirstName = FirstName;
            u.LastName = LastName;
            u.UserType = (eUserType)Enum.Parse(typeof(eUserType), UserType);
            u.ConfirmPassword = ConfirmPassword;
            u.AdditionalInfo = AdditionalInfo;
            if (ScreenAccessList != null) {
                u.ScreenAccessList = ByteArrayToObject(ScreenAccessList);
            } else {
                u.ScreenAccessList = null;
            }
            return u;
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

        List<ScreenAccessDescriber> ByteArrayToObject(byte[] arrBytes) {
            if (arrBytes != null) {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return (List<ScreenAccessDescriber>)obj;
            }
            return null;
        }
    }


    public class UserMySQLService : IUserService {
        readonly SqlDataAccess _db;
        public UserMySQLService(SqlDataAccess db) {
            _db = db;
        }

        public async Task AddUserAsync(User argS) {
            string sql = @"insert into users (UserId , EmailAddress,  Password , FirstName, LastName, UserType, ScreenAccessList, ConfirmPassword, AdditionalInfo)
                           values (@UserId, @EmailAddress,  @Password , @FirstName, @LastName, @UserType, @ScreenAccessList, @ConfirmPassword, @AdditionalInfo);";
            _db.SaveDataAsync(sql, new UserInDB(argS));
        }

        public Task<List<User>> GetUserListAsync() {
            string sql = @"select * from users";
            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;
            List<User> ul = new List<User>();
            foreach (UserInDB uidb in listaPrzejsciowa) {
                ul.Add(uidb.ToUser());
            }
            return Task.FromResult(ul);
        }

        public async Task<User> GetUserAsync(int uid) {
            string sql = "select * from users where UserId=" + uid.ToString();
            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;
            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].ToUser();
                    return usr;
                }
            }
            return null;
        }

        public async Task<User> GetUserWithNameAndPasswordAsync(string email, string password) {
            string sql = string.Format("select * from users where EmailAddress=\"{0}\" and Password=\"{1}\"", email, password);
            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;
            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].ToUser();
                    return usr;
                }
            }
            return null;
        }

        public async Task<User> GetUserAsync(string argEmail) {
            string sql = string.Format("select * from users where EmailAddress=\"{0}\"", argEmail);
            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;
            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].ToUser();
                    return usr;
                }
            }
            return null;
        }

        public async Task<User> LoginAsync(User argUser) {
            User returnedUser = null;
			string psswd_encrypded = EncryptString.StringCipher.Encrypt(argUser.Password);
            returnedUser = await GetUserWithNameAndPasswordAsync(argUser.EmailAddress, psswd_encrypded);
            if (returnedUser != null) {
                Console.WriteLine("returned user is {0}", returnedUser.EmailAddress);
            } else {
                Console.WriteLine("returned user is null");
            }
            return returnedUser;
        }

        public async Task<User> RegisterUserAsync(User argUser) {
            User returnedUser = null;
            User u = await GetUserAsync(argUser.EmailAddress);
            if (u != null) {
                returnedUser = u;
                returnedUser.AdditionalInfo = $"User name {u.EmailAddress} already exists.";
            }
            if (returnedUser == null) {
                if (argUser.Password == argUser.ConfirmPassword) {
                    AddUserAsync(argUser);
                    returnedUser = argUser;
                    returnedUser.AdditionalInfo = null;
                } else {
                    returnedUser = argUser;
                    argUser.AdditionalInfo = "Passwords are not equal!";
                }
            }
            return returnedUser;
        }


        public async Task UpdateUserAsync(User argS) {
            string sql = @" UPDATE users
                        SET EmailAddress = @EmailAddress,  Password = @Password , FirstName = @FirstName, LastName = @LastName, UserType = @UserType, ScreenAccessList = @ScreenAccessList, ConfirmPassword = @ConfirmPassword, AdditionalInfo = @AdditionalInfo
                        WHERE UserId = @UserId ";

            _db.SaveDataAsync(sql, new UserInDB(argS));
        }
    }
}
