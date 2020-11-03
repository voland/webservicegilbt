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
    [Serializable]
    public class DeviceSensor {
        public string unit { get; set; }
        public string name { get; set; }
        public IList<Datum> data { get; set; }
        public string display_type { get; set; }
        public Norm norm { get; set; }

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
            return null;
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
