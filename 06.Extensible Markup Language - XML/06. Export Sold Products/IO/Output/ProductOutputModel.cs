using System;
using System.Xml.Serialization;
using ProductShop.Models;

namespace ProductShop.IO.Output
{
    [XmlType("Product")]
    public class ProductOutputModel
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string BuyerName { get; set; }
    }
}
//< Product >
//    < name > Allopurinol </ name >
//    < price > 518.5 </ price >
//    < buyer > Wallas Duffyn </ buyer >   
//</ Product >
