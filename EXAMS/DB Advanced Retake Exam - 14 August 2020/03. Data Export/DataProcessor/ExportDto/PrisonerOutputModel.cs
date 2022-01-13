using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Prisoner")]
    public class PrisonerOutputModel
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("IncarcerationDate")]
        public string IncarcerationDate { get; set; }

        [XmlArray("EncryptedMessages")]
        public MessageOutputModel[] Mails { get; set; }
    }

    [XmlType("Message")]
    public class MessageOutputModel
    {
        [Required]
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}
