using System;
using System.Xml.Serialization;

namespace CarDealer.IO.Input
{
    [XmlType("Supplier")]
    public class SupplierInputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}
