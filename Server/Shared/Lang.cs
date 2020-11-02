using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {

    public enum Languages { PL, ENG }

    public class Lang {
        public Languages _SiteLanguage = Languages.ENG;

        public Lang() { }

        public Languages SiteLanguage {
            get => _SiteLanguage;
            set {
                if (_SiteLanguage != value) {
                    _SiteLanguage = value;
                    LangChanged?.Invoke();
					Console.WriteLine("Language chages");
                }
            }
        }

        public Action LangChanged;

        public string configure {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "configure";
                    case Languages.PL:
                        return "konfiguruj";
                }
                return "";
            }
        }
        public string screenList {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Screen List";
                    case Languages.PL:
                        return "Lista ekranów";
                }
                return "";
            }
        }
        public string usersList {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "User List";
                    case Languages.PL:
                        return "Lista użytkowników";
                }
                return "";
            }
        }
        public string comunes {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Comunes";
                    case Languages.PL:
                        return "Gminy";
                }
                return "";
            }
        }

        public string welcome {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Welcome";
                    case Languages.PL:
                        return "Witaj";
                }
                return "";
            }
        }

        public string logout {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Logout";
                    case Languages.PL:
                        return "wyloguj";
                }
                return "";
            }
        }

        public string name {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Name";
                    case Languages.PL:
                        return "Nazwa";
                }
                return "";
            }
        }

        public string type {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Type";
                    case Languages.PL:
                        return "Typ";
                }
                return "";
            }
        }

        public string resolution {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Resolution";
                    case Languages.PL:
                        return "Rozdzielczość";
                }
                return "";
            }
        }

        public string lastRequest {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Last request";
                    case Languages.PL:
                        return "Ostatnie żądanie";
                }
                return "";
            }
        }

        public string version {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Version";
                    case Languages.PL:
                        return "wersja";
                }
                return "";
            }
        }

        public string contrast {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Contrast";
                    case Languages.PL:
                        return "Kontrast";
                }
                return "";
            }
        }

        public string nightContrast {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Night contrast";
                    case Languages.PL:
                        return "Nocny kontrast";
                }
                return "";
            }
        }

        public string assignComune {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "assign comune";
                    case Languages.PL:
                        return "lokalizacja";
                }
                return "";
            }
        }


        public string saveChanges {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Save changes";
                    case Languages.PL:
                        return "Zapisz zmiany";
                }
                return "";
            }
        }

        public string exit {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Exit";
                    case Languages.PL:
                        return "Wyjście";
                }
                return "";
            }
        }

        public string confOfScreen {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Configuration of screen";
                    case Languages.PL:
                        return "Konfiguracja ekranu";
                }
                return "";
            }
        }

        public string preferFirmwareVer {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Prefered firmware version";
                    case Languages.PL:
                        return "Preferowana wersja firmare-u";
                }
                return "";
            }
        }

        public string oneSensorIdforAll {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "One sensor Id for all pages";
                    case Languages.PL:
                        return "Stałe Id czujnika dla wszystkich stron";
                }
                return "";
            }
        }

        public string unkownCity {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "unknown city";
                    case Languages.PL:
                        return "nieznane miasto";
                }
                return "";
            }
        }

        public string type0ToTurnOffUnification {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Type 0 to turn off one sensor for all";
                    case Languages.PL:
                        return "Wpisz 0 by wyłączyć stałe Id dla wszystkich";
                }
                return "";
            }
        }

        public string confirm {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Confirm";
                    case Languages.PL:
                        return "Potwierdź";
                }
                return "";
            }
        }

        public string ekran {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Screen";
                    case Languages.PL:
                        return "Ekran";
                }
                return "";
            }
        }

        public string slideShow {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "slide-show";
                    case Languages.PL:
                        return "pokaz slajdów";
                }
                return "";
            }
        }

        public string position {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Position";
                    case Languages.PL:
                        return "Pozycja";
                }
                return "";
            }
        }

        public string parameters {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Parameters";
                    case Languages.PL:
                        return "Parametry";
                }
                return "";
            }
        }

        public string result {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Result";
                    case Languages.PL:
                        return "Wynik";
                }
                return "";
            }
        }
        public string load {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Load";
                    case Languages.PL:
                        return "Wczytaj";
                }
                return "";
            }
        }

        public string delete {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Delete";
                    case Languages.PL:
                        return "Usuń";
                }
                return "";
            }
        }
        public string deletePage {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Delete page";
                    case Languages.PL:
                        return "Usuń stronę";
                }
                return "";
            }
        }

        public string addElement {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Add element";
                    case Languages.PL:
                        return "Dodaj element";
                }
                return "";
            }
        }

        public string moveUp {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Move up";
                    case Languages.PL:
                        return "Do góry";
                }
                return "";
            }
        }
        public string moveDown {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Move down";
                    case Languages.PL:
                        return "W dół";
                }
                return "";
            }
        }

        public string timeInSec {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Time in seconds";
                    case Languages.PL:
                        return "Czas [sek.]";
                }
                return "";
            }
        }

        public string addBlankPage {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Add blank page";
                    case Languages.PL:
                        return "Dodaj pustą stronę";
                }
                return "";
            }
        }

        public string addTemplatePage {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Add page based on template";
                    case Languages.PL:
                        return "Dodaj stronę wg. wzoru";
                }
                return "";
            }
        }

        public string dateTime {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Date/Time";
                    case Languages.PL:
                        return "Data/Czas";
                }
                return "";
            }
        }
        public string pressureTemp {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Pressure/Temp.";
                    case Languages.PL:
                        return "Ciśń./Temp.";
                }
                return "";
            }
        }

        public string airQuality {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Air quality";
                    case Languages.PL:
                        return "jakość powietrza";
                }
                return "";
            }
        }
        public string cancel {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Cancel";
                    case Languages.PL:
                        return "Anuluj";
                }
                return "";
            }
        }
        public string deleteFromDB {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Delete from Data Base";
                    case Languages.PL:
                        return "Usuń z bazy danych";
                }
                return "";
            }
        }
        public string confirmDeleteFromDB {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Confirm deleting this screen from Db.";
                    case Languages.PL:
                        return "Czy na pewno usunąć ten ekran z bazy danych?";
                }
                return "";
            }
        }
        public string screen {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Screen";
                    case Languages.PL:
                        return "Ekran";
                }
                return "";
            }
        }
        public string assignedTo {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Assigned to";
                    case Languages.PL:
                        return "Przypisany do";
                }
                return "";
            }
        }
        public string typeComuneNameAndPickIt {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Type comune name and pick it from hints.";
                    case Languages.PL:
                        return "Wpisz nazwę gminy i wybierz z podpowiedzi.";
                }
                return "";
            }
        }
        public string assigne {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Assigne";
                    case Languages.PL:
                        return "Przypisz";
                }
                return "";
            }
        }

        public string userId {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "User Id";
                    case Languages.PL:
                        return "Id Użytkownika";
                }
                return "";
            }
        }

        public string confOfUser {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Configuration of user";
                    case Languages.PL:
                        return "Konfiguracja użytknika";
                }
                return "";
            }
        }
        public string deleteAccount {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Delete account";
                    case Languages.PL:
                        return "Usuń konto";
                }
                return "";
            }
        }
        public string firstName {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "First Name";
                    case Languages.PL:
                        return "Imię";
                }
                return "";
            }
        }
        public string lastName {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Last Name";
                    case Languages.PL:
                        return "Nazwisko";
                }
                return "";
            }
        }
        public string additionalInfo {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Additional info";
                    case Languages.PL:
                        return "Dodatkowe info";
                }
                return "";
            }
        }

        public string changePassword {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Change password";
                    case Languages.PL:
                        return "Zmień hasło";
                }
                return "";
            }
        }
        public string accessedScreens {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Accessed screens";
                    case Languages.PL:
                        return "Dostępne ekrany";
                }
                return "";
            }
        }

        public string remove {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Remove";
                    case Languages.PL:
                        return "Usuń";
                }
                return "";
            }
        }
        public string addScreenAccess {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Add screen access";
                    case Languages.PL:
                        return "Dodaj Dostęp do ekranu";
                }
                return "";
            }
        }

        public string adminHasFullAccess {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "admin has full access.";
                    case Languages.PL:
                        return "admin ma pełny dostęp.";
                }
                return "";
            }
        }

        public string uCantEditThisUser {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Sorry, this is other admin or unknown account you cannot edit it.";
                    case Languages.PL:
                        return "Sorry, to jest inny admin lub nieznane konto. Nie możesz go edytować";
                }
                return "";
            }
        }
        public string yourPassword {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Your password";
                    case Languages.PL:
                        return "Twoje hasło";
                }
                return "";
            }
        }
        public string newPassword {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "New password";
                    case Languages.PL:
                        return "Nowe hasło";
                }
                return "";
            }
        }
        public string repeatNewPassword {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Repeat new password";
                    case Languages.PL:
                        return "Powtórz nowe hasło";
                }
                return "";
            }
        }

        public string comune {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Comune";
                    case Languages.PL:
                        return "Gmina";
                }
                return "";
            }
        }

        public string rUSureUWantDeleteUser {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Are You sure You want to delete user";
                    case Languages.PL:
                        return "Czy na pewno chcesz usunąć użytkownika";
                }
                return "";
            }
        }
        public string rUSureUWantDeleteThisAccount {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Are You sure You want to delete this account?";
                    case Languages.PL:
                        return "Czy na pewno chcesz usunąć to konto?";
                }
                return "";
            }
        }

        public string yourAccountHasBeenDeleted {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Your account has been successfully permanetly deleted. Have a nice day.";
                    case Languages.PL:
                        return "Twoje konto zostało pomyślnie, permanentnie usunięte. Życzymy miłego dnia.";
                }
                return "";
            }
        }

        public string addComune {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Add comune";
                    case Languages.PL:
                        return "Dodaj gminę";
                }
                return "";
            }
        }
        public string savePresAsTemplate {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Save as template";
                    case Languages.PL:
                        return "Zapisz jako szablon";
                }
                return "";
            }
        }
        public string save {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Save";
                    case Languages.PL:
                        return "Zapisz";
                }
                return "";
            }
        }
        public string templateHasBeenSaved {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Template has been successfully saved.";
                    case Languages.PL:
                        return "Szablon pomyślnie zapisano.";
                }
                return "";
            }
        }
        public string templateNameIsEmpty {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Template name is empty.";
                    case Languages.PL:
                        return "Nie wypełniono nazwy szablonu.";
                }
                return "";
            }
        }

        public string loadTemplate {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Load template";
                    case Languages.PL:
                        return "Użyj szablon";
                }
                return "";
            }
        }
        public string users {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Users";
                    case Languages.PL:
                        return "Użytkownicy";
                }
                return "";
            }
        }

        public string preview {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "Preview";
                    case Languages.PL:
                        return "Podgląd";
                }
                return "";
            }
        }
    }
}
