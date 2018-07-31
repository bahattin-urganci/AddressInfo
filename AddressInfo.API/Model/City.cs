using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AddressInfo.API.Model
{
    public class City
    {
        [XmlAttribute("name")]
        public String Name { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlElement("District")]
        public List<District> Districts { get; set; }
        public City()
        {
            Districts = new List<District>();
        }
    }
}
