using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using VaporStore.Data.Models.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class PurchaseInputModel
    {
        [EnumDataType(typeof(PurchaseType))]
        [XmlElement("Type")]
        public string Type { get; set; }

        //[Required]
        //public PurchaseType? Type { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$")]
        [XmlElement("Key")]
        public string Key { get; set; }

        [Required]
        [XmlElement("Card")]
        public string Card { get; set; }

        [Required]
        [XmlElement("Date")]
        public string Date { get; set; }

        [Required]
        [XmlAttribute("title")]
        public string Title { get; set; }
    }
}
//< Purchase title = "Dungeon Warfare 2" >
//     < Type > Digital </ Type >
//     < Key > ZTZ3 - 0D2S - G4TJ </ Key >
//     < Card > 1833 5024 0553 6211 </ Card >
//     < Date > 07 / 12 / 2016 05:49 </ Date >
//</ Purchase >
