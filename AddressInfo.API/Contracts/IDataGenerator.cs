using System;
using System.Collections.Generic;
using System.Text;

namespace AddressInfo.API.Contracts
{
    public interface IDataGenerator<T> where T:class
    {
        T Generate(string filePath);
        T Generate<TKey>(string filePath,Func<Model.City,bool> query=null, Func<Model.City, TKey> sorting = null,string orderBy =null)where TKey:class;
        string Save(T data);

    }
}
