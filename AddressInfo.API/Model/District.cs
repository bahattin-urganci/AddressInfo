using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AddressInfo.API.Model
{
    public class District
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("Zip")]
        public List<Zip> Zips { get; set; }
        public District()
        {
            Zips = new List<Zip>();
        }
    }
}
