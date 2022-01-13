using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.DTO.Input
{
    [XmlType("Car")]
    public class CarInputModel
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TravelledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public CarPartsInputModel[] CarPartsInputModel { get; set; }
    }
}
//< Car >
//    < make > Opel </ make >
//    < model > Astra </ model >
//    < TraveledDistance > 516628215 </ TraveledDistance >
//    < parts >
//      < partId id = "39" />
 
//       < partId id = "62" />
  
//        < partId id = "72" />
   
//   </ parts >
   
//</ Car >