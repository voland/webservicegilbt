using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceGilBT.Shared {

    public enum Languages { PL, ENG }
    public class Lang {
        public static Languages _SiteLanguage = Languages.ENG;
        public static Languages SiteLanguage {
            get => _SiteLanguage; set {
                if (_SiteLanguage != value) {
                    _SiteLanguage = value;
                    LangChanged.Invoke();
                }
            }
        }


        public static Action LangChanged;

        public static string configure {
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
        public static string screenList {
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
        public static string usersList {
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
        public static string comunes {
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

        public static string welcome {
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

        public static string logout {
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

        public static string name {
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

        public static string type {
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

        public static string resolution {
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

        public static string lastRequest {
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

        public static string version {
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

        public static string contrast {
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

        public static string nightContrast {
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

        public static string assignComune {
            get {
                switch (SiteLanguage) {
                    case Languages.ENG:
                        return "assign comune";
                    case Languages.PL:
                        return "przypisz gminę";
                }
                return "";
            }
        }


        public static string saveChanges {
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

        public static string exit {
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

        public static string confOfScreen {
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

        public static string preferFirmwareVer {
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

        public static string oneSensorIdforAll {
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

        public static string unkownCity {
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

        public static string type0ToTurnOffUnification {
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

        public static string confirm {
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

        public static string ekran {
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

        public static string slideShow {
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

        public static string position {
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

        public static string parameters {
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

        public static string result {
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
        public static string load {
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

        public static string delete {
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
        public static string deletePage {
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

        public static string addElement {
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

        public static string moveUp {
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
        public static string moveDown {
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

        public static string timeInSec {
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

        public static string addBlankPage {
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

        public static string addTemplatePage {
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

        public static string dateTime {
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
        public static string pressureTemp {
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

        public static string airQuality {
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
        public static string cancel {
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
        public static string deleteFromDB {
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
        public static string confirmDeleteFromDB {
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
        public static string screen {
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
        public static string assignedTo {
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
        public static string typeComuneNameAndPickIt {
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
        public static string assigne {
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

        public static string userId {
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

        public static string confOfUser {
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
        public static string deleteAccount {
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
        public static string firstName {
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
        public static string lastName {
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
        public static string additionalInfo {
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

        public static string changePassword {
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
        public static string accessedScreens {
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

        public static string remove {
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
        public static string addScreenAccess {
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

        public static string adminHasFullAccess {
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

        public static string uCantEditThisUser {
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
        public static string yourPassword {
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
        public static string newPassword {
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
        public static string repeatNewPassword {
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

        public static string comune {
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

        public static string rUSureUWantDeleteUser {
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
        public static string rUSureUWantDeleteThisAccount {
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

        public static string yourAccountHasBeenDeleted {
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

        public static string addComune {
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
        public static string savePresAsTemplate {
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
        public static string save {
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
        public static string templateHasBeenSaved {
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
        public static string templateNameIsEmpty {
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

        public static string loadTemplate {
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
        public static string users {
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

        public static string preview {
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
