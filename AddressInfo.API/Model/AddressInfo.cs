using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AddressInfo.API.Model
{
    [Serializable]
    public class AddressInfo
    {
        [XmlElement("City")]
        public List<City> Cities { get; set; }
        public AddressInfo()
        {
            Cities = new List<City>();
        }
    }
}
