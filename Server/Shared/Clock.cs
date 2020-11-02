using System;

namespace WebServiceGilBT.Shared {
    class MyClock {
        static public DateTime Now {
            get {
                TimeZoneInfo tz;
                try {
                    tz = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
                } catch {
                    tz = TimeZoneInfo.FindSystemTimeZoneById("Poland");
                }
                DateTime now = DateTime.UtcNow;
                return TimeZoneInfo.ConvertTime(now, tz);
            }
        }

    }
}
