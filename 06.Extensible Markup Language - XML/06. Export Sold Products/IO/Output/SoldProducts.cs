using System.Xml.Serialization;

namespace ProductShop.IO.Output
{
    [XmlType("Product")]
    public class SoldProducts
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
//< User >
//    < firstName > Almire </ firstName >
//    < lastName > Ainslee </ lastName >
//    < soldProducts >
//       < Product >
//           < name > olio activ mouthwash</name>
//           <price>206.06</price>
//       </Product>
//    </soldProducts >
//</User >