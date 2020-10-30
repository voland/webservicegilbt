using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Data {
    public class Gmina {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NazwaGminy { get; set; }

        [Required]
        public string NazwaPowiatu { get; set; }

        [Required]
        public string NazwaWojewodztwa { get; set; }

        public string stringPodpowiedzi { get => string.Format("{0} , {1} , {2}", NazwaGminy, NazwaPowiatu, NazwaWojewodztwa); }

        public Gmina Copy() {
            return new Gmina() {
                Id = Id,
                NazwaGminy = NazwaGminy,
                NazwaPowiatu = NazwaPowiatu,
                NazwaWojewodztwa = NazwaWojewodztwa,
                uzytkownicy = new List<User>(uzytkownicy),
                ekrany = new List<Screen>(ekrany)
            };
        }

        public List<User> uzytkownicy = new List<User>();

        public List<Screen> ekrany = new List<Screen>();

    }
}
