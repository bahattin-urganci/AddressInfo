using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AddressInfo.API.Extensions
{
    public static class XmlProcessor
    {
        public static string SerializeXML<TObject>(TObject Obj) where TObject : class
        {
            string xml = "";
            XmlDocument xdoc = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(typeof(TObject));
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            using (StringWriter stream = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, Obj);                
                xml = stream.ToString();
            }
            xdoc = null;
            serializer = null;

            return xml;
        }

        public static TObject DeserializeXML<TObject>(string filePath) where TObject : class
        {
            TObject obj;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(TObject));
            using (Stream stream = new MemoryStream())
            using (var reader = XmlReader.Create(filePath))
            {
                xdoc.Save(stream);
                stream.Position = 0;
                obj = serializer.Deserialize(stream) as TObject;
            }

            return obj;
        }

        public static string SaveXML<TObject>(string fileName, TObject data) where TObject : class
        {
            string s = SerializeXML(data);
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(s);
            if (!Directory.Exists("Outputs"))
            {
                Directory.CreateDirectory("Outputs"); 
            }
            fileName += ".xml";
            xdoc.Save($"Outputs\\{fileName}");
            return fileName;
        }
    }
}
