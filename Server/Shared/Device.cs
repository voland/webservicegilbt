using System;
using System.Collections.Generic;

namespace WebServiceGilBT.Shared {
    public class Datum {
        public DateTime read_at { get; set; }
        public double value { get; set; }
        public string current_norm { get; set; }
        public int? threshold_level { get; set; }
    }

    public class Grade {
        public int gte { get; set; }
        public int lt { get; set; }
    }

    public class Norm {
        public int threshold { get; set; }
        public Grade grade_a { get; set; }
        public Grade grade_b { get; set; }
        public Grade grade_c { get; set; }
        public Grade grade_d { get; set; }
        public Grade grade_e { get; set; }
        public Grade grade_f { get; set; }
    }

    public class DeviceSensor {
        public string unit { get; set; }
        public string name { get; set; }
        public IList<Datum> data { get; set; }
        public string display_type { get; set; }
        public Norm norm { get; set; }
    }

    public class Device {
        public IList<double> coordinates { get; set; }
        public int id { get; set; }
        public int source { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public IList<DeviceSensor> sensors { get; set; }
        public bool send_reports { get; set; }
    }
}
