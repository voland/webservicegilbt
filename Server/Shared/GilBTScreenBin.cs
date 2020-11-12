using System;
using System.Collections.Generic;
using System.IO;

namespace WebServiceGilBT.Shared {
    [Serializable]
    public class ScreenBin {
        public int uid { set; get; }
        public string name { set; get; }
        public string firmware_ver { set; get; }
        public int contrast { set; get; }
        public int contrast_night { set; get; }
        public int contrast_max { set; get; } = 4;
        public eScreenType screen_type { set; get; }
        public int width { set; get; }
        public int height { set; get; }
        public bool dhcp { set; get; }
        public string ip { set; get; }
        public string ma { set; get; }
        public string gw { set; get; }
        public byte[] pres { set; get; } //byte array representation of presentation
        public ScreenBin() { }
    }

    [Serializable]
    public class PresBin {
        public byte ver { get { return 2; } }
        public byte pg_cnt_ro;
        public List<PageBin> pgs { get; set; } //pages
        public PresBin() { }

        public byte[] ToByteArray() {
            byte[] retval = null;
            using (MemoryStream ms = new MemoryStream()) {
                using (BinaryWriter bw = new BinaryWriter(ms)) {
                    bw.Write(ver);
                    bw.Write(pg_cnt_ro);
                    foreach (PageBin ps in pgs) {
                        ps.Serialize(bw);
                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    retval = new byte[ms.Length];
                    ms.Read(retval, 0, (int)ms.Length);
                }
            }
            return retval;
        }
    }

    [Serializable]
    public class PageBin {
        public byte ver { get { return 2; } }
        public ushort time { set; get; } //time duration
        public byte el_cnt_ro { set; get; }
        public List<PageElementBin> elements { set; get; }
        public PageBin() { }

        public void Serialize(BinaryWriter bw) {
            if (bw != null) {
                bw.Write(ver);
                bw.Write(time);
                bw.Write(el_cnt_ro);
                foreach (PageElementBin pes in elements) {
                    pes.Serialize(bw);
                }
            }
        }
    }

    [Serializable]
    public class PageElementBin {
        public byte ver { get { return 2; } }
        public byte type { set; get; }
        public ushort x { set; get; } //x position
        public ushort y { set; get; } //y position
        public uint color { set; get; } //colour
        public byte font { set; get; } //font
        public string text { set; get; } //text

        public PageElementBin() { }

        public void Serialize(BinaryWriter bw) {
            if (bw != null) {
                bw.Write(ver);
                bw.Write(type);
                bw.Write(x);
                bw.Write(y);
                bw.Write(color);
                bw.Write(font);
                bw.Write(text);
            }
        }

    }

}
