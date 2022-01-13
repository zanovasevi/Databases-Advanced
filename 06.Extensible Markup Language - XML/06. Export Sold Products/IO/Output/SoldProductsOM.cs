using System.Xml.Serialization;

namespace ProductShop.IO.Output
{
    [XmlType("SoldProducts")]
    public class SoldProductsOM
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ProductOM[] Products { get; set; }
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