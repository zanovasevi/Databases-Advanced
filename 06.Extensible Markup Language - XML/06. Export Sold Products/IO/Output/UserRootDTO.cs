using System;
using System.Xml.Serialization;

namespace ProductShop.IO.Output
{
    [XmlType("Users")]
    public class UserRootDTO
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public UserProductOutputModel[] Users { get; set; }
    }
}
