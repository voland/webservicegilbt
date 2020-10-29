using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebServiceGilBT.Services {
    public class BASerialization {

        public static byte[] objectToByteArray(object obj) {
            if (obj != null) {
                BinaryFormatter binform = new BinaryFormatter();
                MemoryStream fs = new MemoryStream();
                binform.Serialize(fs, obj);
                fs.Close();
                return fs.ToArray();
            }
            return null;
        }

        public static T ByteArrayToObject<T>(byte[] arrBytes) {
            if (arrBytes != null) {
                MemoryStream memStream = new MemoryStream();
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return (T)obj;
            }
            return default(T);
        }
    }
}
