using System;
using System.ComponentModel.DataAnnotations;
using WebServiceGilBT.Services;
using WebServiceGilBT.Shared;

namespace WebServiceGilBT.Data {
    public class PresTemplate {
        public const string tableName = "presTemplateTable";

        [Key]
        public int Id { get; set; }

        public string TemplateName { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public eScreenType ScreenType { get; set; }

        [Required]
        public int UserAuthorId { get; set; }

        public byte[] Pres {
            get { return BASerialization.objectToByteArray(prezentacja); }
            set {
                try { prezentacja = BASerialization.ByteArrayToObject<Pres>(value); } catch { Console.WriteLine("błąd podczas deserializacji byte[] do Pres"); }
            }
        }

        public Pres prezentacja;

        public string authorName;

        public string CreateDate { get; set; }
    }
}
