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

        public Screen() { }

	public string resolution() {
	    return $"{width}x{height}";
	}

	public string contrast_str(){
	    return $"{contrast}/{contrast_max}";
	}
	
	public string night_contrast_str(){
	    return $"{contrast_night}/{contrast_max}";
	}

	public string screen_type_str(){
	    switch ( screen_type){
		case eScreenType.rgb: return "Rgb";
		case eScreenType.mono: return "Mono";
		case eScreenType.unknown: return "Unknown";
	    }
	    return "unknonw";
	}
    }
}
