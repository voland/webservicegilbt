using System.Collections.Generic;
using System.Net;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebServiceGilBT.Shared {
    [Serializable]
    public enum ElementType {
        TEXT = 0,
        RECTANGLE = 1,
        IMAGE = 2,
        TIME = 3,
        DATE = 4,
        SENSOR_TEMPERATURE = 5,
        SENSOR_HUMIDITY = 6,
        SENSOR_PRESSURE = 7,
        SENSOR_PM2_5 = 8,
        SENSOR_PM10 = 9,
        SENSOR_PM1 = 10,
        UID = 11,
        SENSOR_PM2_5_PERCENT,
        SENSOR_PM10_PERCENT,
        SENSOR_PM1_PERCENT,
        SENSOR_PM2_5_STATUS,
        SENSOR_PM10_STATUS,
        SENSOR_PM1_STATUS
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
        /*  Sensor                     */
        /*******************************/
        private int _idx;
        public int idx {
            set {
                _idx = value;
            }
            get {
                return _idx;
            }
        }

        private const string unknowncity = "unknown city";

        [JsonIgnore]
        private Device _device {
            get {
                return DeviceList.GetDeviceById(idx);
            }
        }

        public string city {
            get { return _device.city; }
        }

        private string GenerateSensorText(string sensor_name) {
            //setting default sensor in case its 0;
            if (idx < 0) idx = 444;
            try {
                //innitial value of retvalue
                string retvalue = $"cant find {sensor_name} sensor.";
                if (_device != null) {
                    foreach (DeviceSensor ds in _device.sensors) {
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

        private string uid_for_preview = null;
        public void set_uid_for_preview(string argUid) {
            uid_for_preview = argUid;
        }

        public string text {
            set {
                _text = value;
            }
            get {
                switch (type) {
                    case ElementType.TEXT: return _text;
                    case ElementType.UID:
                        if (uid_for_preview != null)
                            return uid_for_preview.ToString();
                        else
                            return "Screen id number.";
                    case ElementType.TIME: {
                            int h = MyClock.Now.Hour;
                            int m = MyClock.Now.Minute;
                            return string.Format("{0}{1}:{2}{3}", h < 10 ? "0" : "", h, m < 10 ? "0" : "", m);
                        }
                    case ElementType.DATE: {
                            int d = MyClock.Now.Day;
                            int m = MyClock.Now.Month;
                            int y = MyClock.Now.Year;
                            return string.Format("{0}-{1}-{2}", d, m, y);
                        }
                    case ElementType.RECTANGLE: {
                            return "Rectangle not supported";
                        }
                    case ElementType.IMAGE: {
                            return "Image not supported";
                        }
                }
                try {
                    if (_device != null) {
                        switch (type) {
                            case ElementType.SENSOR_PM2_5_PERCENT: {
                                    foreach (DeviceSensor s in _device.sensors) {
                                        if (s.name == pm2_5) {
                                            return s.GetPercentageValue();
                                        }
                                    }
                                    return "Not Found " + pm2_5;
                                }
                            case ElementType.SENSOR_PM10_PERCENT: {
                                    foreach (DeviceSensor s in _device.sensors) {
                                        if (s.name == pm10) {
                                            return s.GetPercentageValue();
                                        }
                                    }
                                    return "Not Found " + pm10;
                                }
                            case ElementType.SENSOR_PM2_5_STATUS: {
                                    foreach (DeviceSensor s in _device.sensors) {
                                        if (s.name == pm2_5) {
                                            return s.GetStatusValue();
                                        }
                                    }
                                    return "Not Found " + pm2_5;
                                }
                            case ElementType.SENSOR_PM10_STATUS: {
                                    foreach (DeviceSensor s in _device.sensors) {
                                        if (s.name == pm10) {
                                            return s.GetStatusValue();
                                        }
                                    }
                                    return "Not Found " + pm10;
                                }
                            case ElementType.SENSOR_PM1_PERCENT: {
                                    foreach (DeviceSensor s in _device.sensors) {
                                        if (s.name == pm1) {
                                            return s.GetPercentageValue();
                                        }
                                    }
                                    return "Not Found " + pm1;
                                }
                            case ElementType.SENSOR_PM1_STATUS: {
                                    foreach (DeviceSensor s in _device.sensors) {
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
                        }
                    } else {
                        return "unknown device";
                    }
                } catch (Exception e) {
                    Debuger.PrintLn(e.Message);
                    return "exception during download sensor";
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

        public static PageElement NewText(string text, int x, int y, uint color, FontType font, string uid) {
            PageElement pe = NewText(text, x, y, color, font);
            pe.set_uid_for_preview(uid);
            return pe;
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
        public int UnifiedIdx { get; set; }
        public int pages_count {
            get { return pages.Count; }
        }
        public List<Page> pages { get; set; }
        public Pres() {
            pages = new List<Page>();
        }

    }
}

