using System;

namespace WebServiceGilBT.Shared {
    class MyClock {
		static private TimeZoneInfo tz= TimeZoneInfo.FindSystemTimeZoneById("Poland");
        static public DateTime Now {
            get {
				DateTime now =  DateTime.UtcNow;
				return TimeZoneInfo.ConvertTime(now, tz);
            }
        }

    }
}
