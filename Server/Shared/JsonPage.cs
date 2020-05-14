/* using System; */
using System.Collections.Generic;

//wersja druga uwzglednia ze element fonttype jest stringiem
namespace WebServiceGilBT.Shared {
    public class JsonPage {
        public uint Ver { get; set; } = 1;
        /* public Element[] Elements { get; set; } */
        public List<Element> Elements { get; set; }
    }

    public class Element {
        public uint Color { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }
        public string Type { get; set; }
        public uint X { get; set; }
        public uint Y { get; set; }
        public string Content { get; set; }
        public uint Fontsize { get; set; }
        public uint Fonttype { get; set; }
    }
}
