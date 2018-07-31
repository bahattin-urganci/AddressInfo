using AddressInfo.API.Contracts;
using AddressInfo.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressInfo.API.Service
{
    public class AddressInfoXmlGenerator : IDataGenerator<Model.AddressInfo>
    {
        public string Save(Model.AddressInfo data) => Extensions.XmlProcessor.SaveXML(Guid.NewGuid().ToString(), data);
        public Model.AddressInfo Generate(string filePath) => Extensions.XmlProcessor.DeserializeXML<Model.AddressInfo>(filePath);

        public Model.AddressInfo Generate<TKey>(string filePath, Func<City, bool> query=null, Func<City, TKey> sorting=null, string orderBy= "ascending") where TKey:class
        {
            var data = Generate(filePath);
            if (query!=null)
            {
                data.Cities = data.Cities.Where(query).ToList(); 
            }
            if (sorting!=null)
            {
                data.Cities = orderBy == "ascending" ? data.Cities.OrderBy(sorting).ToList() : data.Cities.OrderByDescending(sorting).ToList();
            }
           
            return data;
        }

        
    }
}
