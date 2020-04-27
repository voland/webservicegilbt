using System;

namespace WebServiceGilBT.Shared {
    public class Screen : IScreen {
        public int uid { set; get; }
        public string name { set; get; }
        public string firmware_ver { set; get; }
        /* public byte[] firmware_bin { set; get; } */
        public int contrast { set; get; }
        public int contrast_night { set; get; }
        public int contrast_max { set; get; }
        public DateTime last_request { set; get; }
        public eScreenType screen_type { set; get; }
        public int width { set; get; }
        public int height { set; get; }
        public bool dhcp { set; get; }
        public string ip { set; get; }
        public string ma { set; get; }
        public string gw { set; get; }
        public Screen() { }
    }
}
