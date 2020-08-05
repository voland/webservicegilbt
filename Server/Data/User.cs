using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebServiceGilBT.Data;

namespace WebServiceGilBT.Data {

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
        public int UserId { get; set; }
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
                    if (EmailAddress.Contains("@gilbt.com")) {
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
