using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebServiceGilBT.Data;

namespace WebServiceGilBT.Data {

    [Serializable]
    public class ScreenAccessDescriber {
        public string Name { set; get; }
        public int uid { set; get; }
        public bool allowed { set; get; }

        ScreenAccessDescriber(string name, int uid, bool allowed) {
            this.Name = name;
            this.uid = uid;
            this.allowed = allowed;
        }
    }

    public enum eUserType {
        admin,
        normal
    }

    public partial class User {

        private int _userid;
        public int UserId {
            get {
                if (_userid == 0) {
                    Random r = new Random();
                    _userid = r.Next();
                }
                return _userid;
            }
            set {
                _userid = value;
            }
        }

        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private eUserType _ut = eUserType.normal;
        public eUserType UserType {
            set {
                _ut = value;
            }
            get {
                if (EmailAddress != null) {
                    if (EmailAddress == "voland83@gmail.com") {
                        Console.WriteLine(EmailAddress + " certainly admin");
                        return eUserType.admin;
                    }
                    if (EmailAddress.Contains("patryk.brzozowski@syngeos.pl")) {
                        Console.WriteLine(EmailAddress + " certainly admin");
                        return eUserType.admin;
                    }
                }
                return _ut;
            }
        }
        public List<ScreenAccessDescriber> ScreenAccessList { set; get; }
        public string ConfirmPassword { get; set; }
        public string AdditionalInfo { get; set; }

        public bool IsUserAccessedByThisUser(User argEditedUser) {
            if (UserType == eUserType.admin) {
                Console.WriteLine("Youre admin, youre the boss. you can edit user {0}.", argEditedUser.EmailAddress);
                return true;
            }
            if (argEditedUser.UserId == UserId) {
                Console.WriteLine($"Ok I allow to acces myself");
                return true;
            } else {
                Console.WriteLine($"Oh no, you {EmailAddress}, cant access {argEditedUser.EmailAddress}.");
            }
            return false;
        }

        public bool IsScreenAccessedByUser(int uid) {
            if (UserType == eUserType.admin) {
                return true;
            }
            if (ScreenAccessList != null) {
                foreach (ScreenAccessDescriber sad in ScreenAccessList) {
                    if (sad.uid == uid) {
                    }
                }
            }
            return false;
        }
    }
}
