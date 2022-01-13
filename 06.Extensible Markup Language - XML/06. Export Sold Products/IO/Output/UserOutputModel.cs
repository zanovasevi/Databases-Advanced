using System;
using System.Xml.Serialization;

namespace ProductShop.IO.Output
{
    [XmlType("User")]
    public class UserOutputModel
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public SoldProducts[] Products { get; set; }
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
