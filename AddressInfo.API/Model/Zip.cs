using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AddressInfo.API.Model
{
    public class Zip
    {
        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}
