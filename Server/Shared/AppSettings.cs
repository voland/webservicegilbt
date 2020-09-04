using System;
using System.IO;
using System.Text.Json;

namespace WebServiceGilBT.Shared {
    public class LogLevel {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        /* public string Microsoft.Hosting.Lifetime { get; set; } */
    }

    public class Logging {
        public LogLevel LogLevel { get; set; }
    }

    public class ConnectionStrings {
        public string Nazwa { get; set; }
    }

    public class Root {
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class AppSettings {
        public Logging Logging { set; get; }
        public string AllowedHosts { set; get; }
        public ConnectionStrings ConnectionStrings { set; get; }

        private const string filename = "appsettings.json";

        private static AppSettings _as;

        public static AppSettings ReadAppSettings() {
            if (_as == null) {
                try {
                    string serialised_app_settings = File.ReadAllText(filename);
					Console.WriteLine(serialised_app_settings);
                    _as = JsonSerializer.Deserialize<AppSettings>(serialised_app_settings);
                } catch {
                    Debuger.PrintLn("ReadAppSettings: Reading {0} failed.", filename);
                }
            }
            return _as;
        }
    }
}
