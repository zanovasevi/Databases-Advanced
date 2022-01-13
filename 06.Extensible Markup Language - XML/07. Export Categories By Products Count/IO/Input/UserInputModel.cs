﻿using System;
using System.Xml.Serialization;

namespace ProductShop.IO.Input
{
    [XmlType("User")]
    public class UserInputModel
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int Age { get; set; }
    }
}
//< User >
//        < firstName > Chrissy </ firstName >
//        < lastName > Falconbridge </ lastName >
//        < age > 50 </ age >
//</ User >