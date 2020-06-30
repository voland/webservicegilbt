using System;

namespace WebServiceGilBT.Shared {
    public class Screen : IScreen {
        public int uid { set; get; }
        public string name { set; get; }
        public string firmware_ver { set; get; }
        /* public byte[] firmware_bin { set; get; } */
        public int contrast { set; get; }
        public int contrast_night { set; get; }
        public int contrast_max { set; get; } = 4;
        public DateTime last_request { set; get; }
        public eScreenType screen_type { set; get; }
        public int width { set; get; }
        public int height { set; get; }
        public bool dhcp { set; get; }
        public string ip { set; get; }
        public string ma { set; get; }
        public string gw { set; get; }
        private Pres _pres = null;
        public Pres pres {
            set {
                _pres = value;
            }
            get {
                if (_pres == null) {
                    _pres = new Pres();

                    Page p1 = new Page(5);
                    p1.elements.Add(new Text("Screen uid:", 0, 0, 1, "FONTNORMAL"));
                    p1.elements.Add(new Text(uid.ToString(), 0, 8, 1, "FONTNORMAL"));
                    _pres.pages.Add(p1);

                    Page p2 = new Page(5);
                    p2.elements.Add(new Text("Time Now:", 0, 0, 1, "FONTFAT"));
                    p2.elements.Add(new Time(0, 8, 1, "FONTNORMAL"));
                    p2.elements.Add(new Date(32, 8, 1, "FONTNORMAL"));
                    _pres.pages.Add(p2);

                    Page p3 = new Page(5);
                    p3.elements.Add(new Text("Miłkowo pm2,5:", 0, 0, 1, "FONTFAT"));
                    p3.elements.Add(new Sensor(444, "pm2_5", 0, 8, 1, "FONTNORMAL"));
                    _pres.pages.Add(p3);

                    Page p4 = new Page(5);
                    p4.elements.Add(new Text("Miłkowo pm10:", 0, 0, 1, "FONTFAT"));
                    p4.elements.Add(new Sensor(444, "pm10", 0, 8, 1, "FONTNORMAL"));
                    _pres.pages.Add(p4);
                }
                return _pres;
            }
        }

        public Screen() { }

        public string resolution() {
            return $"{width}x{height}";
        }

        public string contrast_str() {
            return $"{contrast}/{contrast_max}";
        }

        public string night_contrast_str() {
            return $"{contrast_night}/{contrast_max}";
        }

        public string screen_type_str() {
            switch (screen_type) {
                case eScreenType.rgb: return "Rgb";
                case eScreenType.mono: return "Mono";
                case eScreenType.unknown: return "Unknown";
            }
            return "unknonw";
        }
    }
}
