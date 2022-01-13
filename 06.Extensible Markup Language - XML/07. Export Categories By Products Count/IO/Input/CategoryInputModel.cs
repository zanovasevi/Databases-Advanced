using System;
using System.Xml.Serialization;

namespace ProductShop.IO.Input
{
    [XmlType("Category")]
    public class CategoryInputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
