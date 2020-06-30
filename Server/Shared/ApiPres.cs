using System.Collections.Generic;
using System.Net;
using System;
using System.Text.Json;

namespace WebServiceGilBT.Shared {

    enum ElementType {
        TEXT,
        RECTANGLE,
        IMAGE,
        SENSOR,
        TIME,
        DATE
    }

    static public class FontNames {
        public const string fontfat = "FONTFAT";
        public const string fontnormal = "FONTNORMAL";
    }

    public class PageElement {
        public int ver { get { return 2; } }
        public virtual int type { set; get; }
        public int x { set; get; }
        public int y { set; get; }
        public uint color { set; get; }
        public string fontname { set; get; }
        public virtual string text { set; get; }

        public static PageElement NewText(string text, int x, int y, uint color, string fontname) {
            PageElement temp = new PageElement();
            temp.type = (int)ElementType.TEXT;
            temp.x = x;
            temp.y = y;
            temp.color = color;
            temp.text = text;
            temp.fontname = fontname;
            return temp;
        }

        public PageElement() { }

        public PageElement(string text, int x, int y, uint color, string fontname) {
            this.type = (int)ElementType.TEXT;
            this.x = x;
            this.y = y;
            this.color = color;
            this.text = text;
            this.fontname = fontname;
        }
    }

    public class Text : PageElement {
        public virtual int type { get { return (int)ElementType.TEXT; } }
        public Text() { }
        public Text(string text, int x, int y, uint color, string fontname) : base(text, x, y, color, fontname) {
        }
    }

    public class Time : PageElement {
        public virtual int type { get { return (int)ElementType.TIME; } }

        public override string text {
            get {
                DateTime now = DateTime.Now;
#if DEBUG
#else
				//dodajemy 2 h dla serwera gdzies za granica
				now  = now.AddHours(2);
#endif

                int h = now.Hour;
                int m = now.Minute;
                return string.Format("{0}{1}:{2}{3}", h < 10 ? "0" : "", h, m < 10 ? "0" : "", m);
            }
        }

        public Time() { }
        public Time(int x, int y, uint color, string fontname) : base("", x, y, color, fontname) {
        }
    }

    public class Date : PageElement {
        public virtual int type { get { return (int)ElementType.DATE; } }
        public override string text {
            get {
                DateTime now = DateTime.Now;
#if DEBUG
#else
				//dodajemy 2 h dla serwera gdzies za granica
				now = now.AddHours(2.0);
#endif
                int d = now.Day;
                int m = now.Month;
                int y = now.Year;
                return string.Format("{0}-{1}-{2}", d, m, y);
            }
        }

        public Date() { }
        public Date(int x, int y, uint color, string fontname) : base("", x, y, color, fontname) {
        }
    }

    public class Sensor : PageElement {
        public int idx { set; get; }

        public string name { set; get; }

        public string city { set; get; }

        private string url = "https://api.syngeos.pl/api/public/data/device/{0}";

        private string GetUrl() {
            return string.Format(url, idx);
        }

        public virtual int type { get { return (int)ElementType.SENSOR; } }

        public override string text {
            get {
                if (idx == -1) {
                    //find idx based on city
                    if (city == "") {
                        return "SensFail";
                    } else {
                    }
                }

                using (WebClient wc = new WebClient()) {
                    var json = wc.DownloadString(GetUrl());
                    Device device = JsonSerializer.Deserialize<Device>(json);
                    foreach (DeviceSensor ds in device.sensors) {
                        if (ds.name == name) {
                            string output = string.Format("{0} {1}", ds.data[0].value, ds.unit);
                            /* Console.WriteLine(output); */
                            return output;
                        }
                    }
                    /* Console.WriteLine($"cant find {name}"); */
                    return $"cant find {name}";
                }
            }
        }

        public Sensor(int idx, string sensor_name, int x, int y, uint color, string fontname) : base("", x, y, color, fontname) {
            this.idx = idx;
            this.name = sensor_name;
        }

        public Sensor(string city, string sensor_name, int x, int y, uint color, string fontname) : base("", x, y, color, fontname) {
            this.city = city;
            this.name = sensor_name;
        }
        public Sensor() { }
    }

    public class Page {
        public int ver { get { return 2; } }
        public int time { set; get; }
        public int elements_count { get { return elements.Count; } }
        public List<PageElement> elements { set; get; }
        //as parameter is duation time in seconds
        public Page(int time) {
            this.time = time;
            elements = new List<PageElement>();
        }
        public Page() { }
		public override string ToString(){
			return String.Format("page: time {0}, elements_count {1}", time, elements.Count);
		}
    }

    public class Pres {
        public int ver { get { return 2; } }
        public int pages_count {
            get { return pages.Count; }
        }
        public List<Page> pages { get; set; }
        public Pres() {
            pages = new List<Page>();
        }

    }
}

