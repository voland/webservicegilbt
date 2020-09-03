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
        //   public List<ScreenAccessDescriber> ScreenAccessList { set; get; }
        public string ConfirmPassword { get; set; }
        public string AdditionalInfo { get; set; }

        public UserInDB() { }
        public UserInDB(User u) {
            UserId = u.UserId;
            EmailAddress = u.EmailAddress;
            Password = u.Password;
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

        public User OddajUsera() {
            User u = new User();
            u.UserId = UserId;
            u.EmailAddress = EmailAddress;
            u.Password = Password;
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

        public Task AddUserAsync(User argS) {
            string sql = @"insert into users (UserId , EmailAddress,  Password , FirstName, LastName, UserType, ScreenAccessList, ConfirmPassword, AdditionalInfo)
                           values (@UserId, @EmailAddress,  @Password , @FirstName, @LastName, @UserType, @ScreenAccessList, @ConfirmPassword, @AdditionalInfo);";



            return _db.SaveData(sql, new UserInDB(argS));
        }

        public Task<List<User>> GetUserListAsync() {
            string sql = @"select * from users";

            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;

            List<User> ul = new List<User>();
            foreach (UserInDB uidb in listaPrzejsciowa) {
                ul.Add(uidb.OddajUsera());
            }
            return Task.FromResult(ul);
        }

        public Task<User> GetSpecificUser(int uid) {
            string sql = "select * from users where UserId=" + uid;

            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;

            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].OddajUsera();
                    return Task.FromResult(usr);
                }
            }
            return null;
        }

        public Task<User> GetUserWithSpecificNameAndPassword(string email, string password) {
            string sql = string.Format("select * from users where EmailAddress={0} and Password={1}", email, password);

            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;

            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].OddajUsera();
                    return Task.FromResult(usr);
                }
            }
            return null;
        }

        public Task<User> GetSpecificUser(string email) {
            string sql = "select * from users where EmailAddress=" + email;

            List<UserInDB> listaPrzejsciowa = _db.LoadData<UserInDB, dynamic>(sql, new { }).Result;

            if (listaPrzejsciowa != null) {
                if (listaPrzejsciowa.Count > 0) {
                    User usr = listaPrzejsciowa[0].OddajUsera();
                    return Task.FromResult(usr);
                }
            }
            return null;
        }

        public Task<User> LoginAsync(User user) {
            User returnedUser = null;

            Console.WriteLine("Checking user {0}", user.EmailAddress);

            returnedUser = GetUserWithSpecificNameAndPassword(user.EmailAddress, user.Password).Result;


            if (returnedUser != null) {
                Console.WriteLine("returned user is {0}", returnedUser.EmailAddress);
            } else {
                Console.WriteLine("returned user is null");
            }
            return Task.FromResult(returnedUser);
        }

        public Task<User> RegisterUserAsync(User user) {

            User returnedUser = null;

            User u = GetSpecificUser(user.EmailAddress).Result;

            if (u != null) {
                returnedUser = u;
                returnedUser.AdditionalInfo = $"User name {u.EmailAddress} already exists.";
            }
            if (returnedUser == null) {
                if (user.Password == user.ConfirmPassword) {
                    AddUserAsync(user);
                    Console.WriteLine("Adding user {0} password {1}", user.EmailAddress, user.Password);
                    returnedUser = user;
                    returnedUser.AdditionalInfo = null;
                } else {
                    returnedUser = user;
                    user.AdditionalInfo = "Passwords are not equal!";
                }
            }
            return Task.FromResult(returnedUser);

        }


        public Task UpdateUserAsync(User argS) {

            string sql = @" UPDATE users
                        SET EmailAddress = @EmailAddress,  Password = @Password , FirstName = @FirstName, LastName = @LastName, UserType = @UserType, ScreenAccessList = @ScreenAccessList, ConfirmPassword = @ConfirmPassword, AdditionalInfo = @AdditionalInfo
                        WHERE UserId = @UserId ";

            return _db.SaveData(sql, new UserInDB(argS));
        }
    }
}
