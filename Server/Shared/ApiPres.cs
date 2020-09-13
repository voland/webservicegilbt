using System.Collections.Generic;
using System.Net;
using System;
using System.Text.Json;

namespace WebServiceGilBT.Shared {
    [Serializable]
    public enum ElementType {
        TEXT,
        RECTANGLE,
        IMAGE,
        TIME,
        DATE,
        SENSOR_TEMPERATURE,
        SENSOR_HUMIDITY,
        SENSOR_PRESSURE,
        SENSOR_PM2_5,
        SENSOR_PM10,
        SENSOR_PM1,
        SENSOR_PM2_5_PERCENT,
        SENSOR_PM10_PERCENT,
        SENSOR_PM1_PERCENT,
        SENSOR_PM2_5_STATUS,
        SENSOR_PM10_STATUS,
        SENSOR_PM1_STATUS,
        UID
    }
    [Serializable]
    public enum FontType {
        fontnormal8px,
        fontfat8px,
        arial14,
        arial16,
        impact14,
        impact16
    }

    [Serializable]
    static public class FontNames {
        public const string fontfat = "FONTFAT";
        public const string fontnormal = "FONTNORMAL";
    }

    [Serializable]
    public class PageElement {
        //sensornames
        const string temperature = "temperature";
        const string humidity = "humidity";
        const string air_pressure = "air_pressure";
        const string pm2_5 = "pm2_5";
        const string pm10 = "pm10";
        const string pm1 = "pm1";

        public int ver { get { return 2; } }
        public ElementType type { set; get; }
        public int x { set; get; }
        public int y { set; get; }
        public uint color { set; get; }
        public FontType font { set; get; }
        private string _text = "Text...";
        /*******************************/
        /*  Date Time                  */
        /*******************************/
        private DateTime Now {
            get {
#if DEBUG
                return DateTime.Now;
#else
				return DateTime.Now.AddHours(2);
#endif
            }
        }
        /*******************************/
        /*  Sensor                     */
        /*******************************/
        private int _idx;
        public int idx {
            set {
                if (value != _idx) {
                    _idx = value;
                    try {
                        using (WebClient wc = new WebClient()) {
                            var json = wc.DownloadString(GetUrl());
                            d = JsonSerializer.Deserialize<Device>(json);
                            if (d != null) {
                                _city = d.city;
                            }
                        }
                    } catch {
                        _city = unknowncity;
                    }
                }
            }
            get {
                return _idx;
            }
        }
        private string url = "https://api.syngeos.pl/api/public/data/device/{0}";
        private const string unknowncity = "unknown city";
        private string _city = unknowncity;
        private Device d = null;
        private DateTime last_read_device;

        private string GetUrl() {
            return string.Format(url, idx);
        }

        public string city {
            get { return _city; }

            set { _city = value; }
        }

        private string GenerateSensorText(string sensor_name) {
            if (last_read_device == null) last_read_device = DateTime.Now.AddHours(-100);
            //setting default sensor in case its 0;
            if (idx < 0) idx = 444;
            if (idx == -1) {
                //find idx based on city
                if (city == "") {
                    return "SensFail";
                } else {
                }
            }

            try {
                if ((d == null) | (DateTime.Now > last_read_device.AddHours(1))) {
                    using (WebClient wc = new WebClient()) {
                        last_read_device = DateTime.Now;
                        var json = wc.DownloadString(GetUrl());
                        d = JsonSerializer.Deserialize<Device>(json);
                    }
                }
                //innitial value of retvalue
                string retvalue = $"cant find {sensor_name} sensor.";
                if (d != null) {
                    /* city = d.city; */
                    foreach (DeviceSensor ds in d.sensors) {
                        if (ds.name == sensor_name) {
                            retvalue = string.Format("{0} {1}", ds.data[0].value, ds.unit);
                        }
                    }
                }
                return retvalue;
            } catch {
                return unknowncity;
            }
        }

        public string text {
            set {
                _text = value;
            }
            get {
                switch (type) {
                    case ElementType.TEXT:
                        return _text;
                    case ElementType.TIME: {
                            int h = Now.Hour;
                            int m = Now.Minute;
                            return string.Format("{0}{1}:{2}{3}", h < 10 ? "0" : "", h, m < 10 ? "0" : "", m);
                        }
                    case ElementType.DATE: {
                            int d = Now.Day;
                            int m = Now.Month;
                            int y = Now.Year;
                            return string.Format("{0}-{1}-{2}", d, m, y);
                        }
                    case ElementType.RECTANGLE: {
                            return "Rectangle not supported";
                        }
                    case ElementType.IMAGE: {
                            return "Image not supported";
                        }
                    case ElementType.SENSOR_PM2_5_PERCENT: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm2_5) {
                                    return s.GetPercentageValue();
                                }
                            }
                            return "Not Found " + pm2_5;
                        }
                    case ElementType.SENSOR_PM10_PERCENT: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm10) {
                                    return s.GetPercentageValue();
                                }
                            }
                            return "Not Found " + pm10;
                        }
                    case ElementType.SENSOR_PM2_5_STATUS: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm2_5) {
                                    return s.GetStatusValue();
                                }
                            }
                            return "Not Found " + pm2_5;
                        }
                    case ElementType.SENSOR_PM10_STATUS: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm10) {
                                    return s.GetStatusValue();
                                }
                            }
                            return "Not Found " + pm10;
                        }
                    case ElementType.SENSOR_PM1_PERCENT: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm1) {
                                    return s.GetPercentageValue();
                                }
                            }
                            return "Not Found " + pm1;
                        }
                    case ElementType.SENSOR_PM1_STATUS: {
                            foreach (DeviceSensor s in d.sensors) {
                                if (s.name == pm1) {
                                    return s.GetStatusValue();
                                }
                            }
                            return "Not Found " + pm1;
                        }
                    case ElementType.SENSOR_PM2_5: {
                            return GenerateSensorText(pm2_5);
                        }
                    case ElementType.SENSOR_PM10: {
                            return GenerateSensorText(pm10);
                        }
                    case ElementType.SENSOR_TEMPERATURE: {
                            return GenerateSensorText(temperature);
                        }
                    case ElementType.SENSOR_PRESSURE: {
                            return GenerateSensorText(air_pressure);
                        }
                    case ElementType.SENSOR_HUMIDITY: {
                            return GenerateSensorText(humidity);
                        }
                    case ElementType.SENSOR_PM1: {
                            return GenerateSensorText(pm1);
                        }
                    case ElementType.UID: {
                            return "Screen id number.";
                        }
                }
                return "element not supported!";
            }
        }

        public static PageElement NewSensorPm2_5(int idx, int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.SENSOR_PM2_5;
            temp.idx = idx;
            return temp;
        }

        public static PageElement NewSensorPm10(int idx, int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.SENSOR_PM10;
            temp.idx = idx;
            return temp;
        }

        public static PageElement NewSensorTemperature(int idx, int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.SENSOR_TEMPERATURE;
            temp.idx = idx;
            return temp;
        }

        public static PageElement NewSensorPressure(int idx, int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.SENSOR_PRESSURE;
            temp.idx = idx;
            return temp;
        }

        public static PageElement NewTime(int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.TIME;
            return temp;
        }

        public static PageElement NewDate(int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.DATE;
            return temp;
        }

        public static PageElement NewUid(int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement("", x, y, color, font);
            temp.type = ElementType.UID;
            return temp;
        }

        public static PageElement NewText(string text, int x, int y, uint color, FontType font) {
            PageElement temp = new PageElement();
            temp.type = ElementType.TEXT;
            temp.x = x;
            temp.y = y;
            temp.color = color;
            temp.text = text;
            temp.font = font;
            return temp;
        }

        public PageElement() { }

        public PageElement(string text, int x, int y, uint color, FontType font) {
            this.type = ElementType.TEXT;
            this.x = x;
            this.y = y;
            this.color = color;
            this.text = text;
            this.font = font;
        }
    }

    [Serializable]
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
        public override string ToString() {
            return String.Format("page: time {0}, elements_count {1}", time, elements.Count);
        }
    }

    [Serializable]
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

