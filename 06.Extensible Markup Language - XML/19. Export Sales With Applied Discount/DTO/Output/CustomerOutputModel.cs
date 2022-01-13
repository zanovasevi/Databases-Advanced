using System;
using System.Xml.Serialization;

namespace CarDealer.DTO.Output
{
    [XmlType("customer")]
    public class CustomerOutputModel
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
//< customers >

//     < customer full - name = "Hai Everton" bought - cars = "1" spent - money = "2544.67" />
         
//     < customer full - name = "Daniele Zarate" bought - cars = "1" spent - money = "2014.83" />
                  
//     < customer full - name = "Donneta Soliz" bought - cars = "1" spent - money = "1655.57" />

//</ customers >