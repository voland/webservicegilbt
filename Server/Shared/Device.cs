using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Net;
using System.Text.Json;

namespace WebServiceGilBT.Shared {
    [Serializable]
    public class Datum {
        public DateTime read_at { get; set; }
        public double value { get; set; }
        public string current_norm { get; set; }
        public int? threshold_level { get; set; }
    }
    [Serializable]
    public class Grade {
        public int gte { get; set; }
        public int lt { get; set; }
    }
    [Serializable]
    public class Norm {
        public int threshold { get; set; }
        public Grade grade_a { get; set; }
        public Grade grade_b { get; set; }
        public Grade grade_c { get; set; }
        public Grade grade_d { get; set; }
        public Grade grade_e { get; set; }
        public Grade grade_f { get; set; }
    }

    public enum eAirQualityLevel {
        nieznany,
        bardzo_zly,
        zly,
        dostateczny,
        umiarkowany,
        dobry,
        bardzo_dobry
    }

    [Serializable]
    public class DeviceSensor {
        public string unit { get; set; }
        public string name { get; set; }
        public IList<Datum> data { get; set; }
        public string display_type { get; set; }
        public Norm norm { get; set; }

        public eAirQualityLevel GetAirQualityLevel() {
            if (norm != null) {
                eAirQualityLevel retvalue = eAirQualityLevel.bardzo_zly;
                int value = (int)data[0].value;
                if (value <= norm.grade_e.lt) retvalue = eAirQualityLevel.zly;
                if (value <= norm.grade_d.lt) retvalue = eAirQualityLevel.dostateczny;
                if (value <= norm.grade_c.lt) retvalue = eAirQualityLevel.umiarkowany;
                if (value <= norm.grade_b.lt) retvalue = eAirQualityLevel.dobry;
                if (value <= norm.grade_a.lt) retvalue = eAirQualityLevel.bardzo_dobry;
                return retvalue;
            } else {
                return eAirQualityLevel.nieznany;
            }
        }

        public string GetEmotFromAwesomeFont() {
            switch (GetAirQualityLevel()) {
                case eAirQualityLevel.bardzo_zly:
                    return "fas fa-smile";
                case eAirQualityLevel.zly:
                    return  "fas fa-smile";
                case eAirQualityLevel.dostateczny:
                    return  "fas fa-smile";
                case eAirQualityLevel.umiarkowany:
                    return  "fas fa-smile";
                case eAirQualityLevel.dobry:
                    return  "fas fa-smile";
                case eAirQualityLevel.bardzo_dobry:
                    return  "fas fa-smile";
            }
            return "Z";
        }

        public string GetPercentageValue() {
            if (norm != null) {
                int value = (int)data[0].value;
                int threshold = norm.threshold;
                int percent_value = (100 * value) / threshold;
                return string.Format("{0}%", percent_value);
            } else {
                return "Unknown percent.";
            }
        }

        public const uint bardzo_zly_colo = ((uint)0xff << 24) | (174 << 16) | (0 << 8) | (14);
        public const uint zly_colo = ((uint)0xff << 24) | (232 << 16) | (2 << 8) | (62);
        public const uint dostateczny_colo = ((uint)0xff << 24) | (253 << 16) | (125 << 8) | (81);
        public const uint umiarkowany_colo = ((uint)0xff << 24) | (251 << 16) | (238 << 8) | (74);
        public const uint dobry_colo = ((uint)0xff << 24) | (189 << 16) | (235 << 8) | (104);
        public const uint bardzo_dobry_colo = ((uint)0xff << 24) | (81 << 16) | (224 << 8) | (125);

        public uint GetStatusColor() {
            if (norm != null) {
                uint retvalue = bardzo_zly_colo;
                int value = (int)data[0].value;
                if (value <= norm.grade_e.lt) retvalue = zly_colo;
                if (value <= norm.grade_d.lt) retvalue = dostateczny_colo;
                if (value <= norm.grade_c.lt) retvalue = umiarkowany_colo;
                if (value <= norm.grade_b.lt) retvalue = dobry_colo;
                if (value <= norm.grade_a.lt) retvalue = bardzo_dobry_colo;
                return (uint)retvalue;
            } else {
                return 0;
            }
        }

        public string GetStatusValue() {
            if (norm != null) {
                string retvalue = "Bardzo Zły";
                int value = (int)data[0].value;
                if (value <= norm.grade_e.lt) retvalue = "Zły";
                if (value <= norm.grade_d.lt) retvalue = "Dostateczny";
                if (value <= norm.grade_c.lt) retvalue = "Umiarkowany";
                if (value <= norm.grade_b.lt) retvalue = "Dobry";
                if (value <= norm.grade_a.lt) retvalue = "Bardzo dobry";
                return retvalue;
            } else {
                return "Unknown status.";
            }
        }
    }

    [Serializable]
    public class Device {
        public IList<double> coordinates { get; set; }
        public int id { get; set; }
        public int source { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public IList<DeviceSensor> sensors { get; set; }
        public bool send_reports { get; set; }
        [JsonIgnore]
        public DateTime last_read_device { set; get; }
    }

    public class DeviceList {
        private static List<Device> devices;

        private static string url = "https://api.syngeos.pl/api/public/data/device/{0}";

        private static string GetDeviceUrl(int id) {
            return string.Format(url, id);
        }

        private static Device DownloadDevice(int id) {
            try {
                using (WebClient wc = new WebClient()) {
                    var json = wc.DownloadString(GetDeviceUrl(id));
                    Device _device = JsonSerializer.Deserialize<Device>(json);
                    _device.last_read_device = MyClock.Now;
                    return _device;
                }
            } catch (Exception e) {
                Debuger.PrintLn(e.Message);
                Device _device = new Device();
                _device.city = "unknown city";
                _device.id = id;
                _device.last_read_device = MyClock.Now;
                _device.sensors = new List<DeviceSensor>();
                return _device;
            }
        }

        private static object locker = new Object();

        public static Device GetDeviceById(int id) {
            lock (locker) {
                Device retval = null;
                if (devices == null) devices = new List<Device>();
                foreach (Device d in devices) if (d.id == id) retval = d;
                if (retval == null) {
                    Device d = DownloadDevice(id);
                    Debuger.PrintLn("Add new device {0}", d.city);
                    devices.Add(d);
                    return d;
                }
                if (MyClock.Now > retval.last_read_device.AddHours(1)) {
                    Device d = DownloadDevice(id);
                    devices.Remove(retval);
                    Debuger.PrintLn("Time elapsed for device {0}", retval.city);
                    devices.Add(d);
                    return d;
                }
                return retval;
            }
        }
    }
}
