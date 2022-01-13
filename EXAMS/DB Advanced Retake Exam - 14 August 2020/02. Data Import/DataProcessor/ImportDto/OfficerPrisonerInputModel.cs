using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using SoftJail.Data.Models;
using SoftJail.Data.Models.Enums;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerPrisonerInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [XmlElement("Money")]
        public decimal Money { get; set; }

        [EnumDataType(typeof(Position))]
        [XmlElement("Position")]
        public string Position { get; set; }

        [EnumDataType(typeof(Weapon))]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }

        [ForeignKey(nameof(Department))]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public PrisonerInputModel[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class PrisonerInputModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
