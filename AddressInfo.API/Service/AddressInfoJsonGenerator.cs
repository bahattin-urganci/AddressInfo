using AddressInfo.API.Contracts;
using AddressInfo.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressInfo.API.Service
{
    public class AddressInfoJsonGenerator : IDataGenerator<Model.AddressInfo>
    {
        public Model.AddressInfo Generate(string filePath)
        {
            throw new NotImplementedException();
        }

        public Model.AddressInfo Generate<TKey>(string filePath, Func<City, bool> query = null, Func<City, TKey> sorting = null, string orderBy = null) where TKey : class
        {
            throw new NotImplementedException();
        }

        public string Save(Model.AddressInfo data)
        {
            throw new NotImplementedException();
        }
    }
}
