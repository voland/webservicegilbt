using System;
using System.IO;

namespace WebServiceGilBT.Shared {
    public class Firmware {
        public int uid { set; get; }
        public int datasize { set; get; }
        public string filename { set; get; }
        public byte[] data { set; get; }
        public int checksum { set; get; }
        private string path;
        public Firmware(String argFileName) {
            data = new byte[] { 121, 2, 3, 4, 5, 6, 7 };
            path = argFileName;
            filename = Path.GetFileName(path);
            checksum = 0;
            LoadFromFile();
            foreach (byte b in data) {
                checksum += (int)b;
            }
            datasize = data.Length;
        }

        public void SaveToFile() {
            using (FileStream fs = new FileStream(path, FileMode.Create)) {
                using (BinaryWriter bw = new BinaryWriter(fs)) {
                    bw.Write(data);
                }
            }
        }

        private void LoadFromFile() {
            try {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using (BinaryReader br = new BinaryReader(fs)) {
                        long len = new FileInfo(path).Length;
                        data = br.ReadBytes((int)len);
                    }
                }
            } catch {
            }
        }
    }
}
