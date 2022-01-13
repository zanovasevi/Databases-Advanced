using System;
using System.Xml.Serialization;

namespace ProductShop.IO.Output
{
    [XmlType("User")]
    public class UserProductOutputModel
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlArray("SoldProducts")]
        public SoldProductsOM[] SoldProducts { get; set; }
    }
}
//< Users >
//  < count > 54 </ count >
//  < users >
//    < User >
//       < firstName > Cathee </ firstName >
//       < lastName > Rallings </ lastName >
//       < age > 33 </ age >
//       < SoldProducts >
//           < count > 9 </ count >
//           < products >
//              < Product >
//                 < name > Fair Foundation SPF 15</name>
//                 <price>1394.24</price>
//              </Product>
