using System;

namespace WebServiceGilBT.Shared{

    public class Screen: IScreen{
	public int ID { set; get; }
	public string Name { set; get; }
	public string FirmwareVer { set; get; }
	public byte[] FirmwareBin { set; get; }
	public int Contrast { set; get; }
	public int ContrastNight { set; get; }
	public DateTime LastResponse { set; get; }
	public eScreenType ScreenType { set; get; }
	public int Width { set; get; }
	public int Height { set; get; }
	public bool Dhcp { set; get; }
	public string Ip { set; get; }
	public string Ma { set; get; }
	public string Gw { set; get; }
	public Screen(){
	}
    }
}
