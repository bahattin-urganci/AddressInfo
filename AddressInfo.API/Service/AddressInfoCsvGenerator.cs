using AddressInfo.API.Contracts;
using AddressInfo.API.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AddressInfo.API.Service
{
    public class AddressInfoCsvGenerator : IDataGenerator<Model.AddressInfo>
    {
        public Model.AddressInfo Generate(string filePath)
        {
            Model.AddressInfo addressInfo = new Model.AddressInfo();
            var lines = File.ReadAllLines(filePath);
            var csv = from line in lines
                      select (line.Split(',')).ToArray();
            foreach (var item in csv.Skip(1))
            {
                City cityData = new City { Name = item[0], Code = item[1] };
                District districtData = new District { Name = item[2] };
                Zip zipData = new Zip { Code = item[3] };
                var city = addressInfo.Cities.FirstOrDefault(c => c.Name == cityData.Name);
                if (city == null)
                {
                    cityData.Districts.Add(districtData);
                    cityData.Districts[0].Zips.Add(zipData);
                    addressInfo.Cities.Add(cityData);
                }
                else
                {
                    var district = city.Districts.FirstOrDefault(x => x.Name == districtData.Name);
                    if (district == null)
                    {
                        districtData.Zips.Add(zipData);
                        city.Districts.Add(districtData);
                    }
                    else
                    {
                        district.Zips.Add(zipData);
                    }
                }

            }


            return addressInfo;
        }

        public Model.AddressInfo Generate<TKey>(string filePath, Func<City, bool> query = null, Func<City, TKey> sorting = null, string orderBy = "ascending") where TKey : class
        {
            var data = Generate(filePath);
            if (query != null)
            {
                data.Cities = data.Cities.Where(query).ToList();
            }
            if (sorting != null)
            {
                data.Cities = orderBy == "ascending" ? data.Cities.OrderBy(sorting).ToList() : data.Cities.OrderByDescending(sorting).ToList();
            }

            return data;
        }

        public string Save(Model.AddressInfo data)
        {
            string fileName = Guid.NewGuid().ToString();
            StringBuilder sb = new StringBuilder();
            foreach (var city in data.Cities)
            {

                foreach (var district in city.Districts)
                {
                    foreach (var zip in district.Zips)
                    {
                        sb.Append(city.Name);
                        sb.Append(",");
                        sb.Append(city.Code);
                        sb.Append(",");
                        sb.Append(district.Name);
                        sb.Append(",");
                        sb.Append(zip.Code);
                        sb.AppendLine();
                    }
                }
            }
            if (!Directory.Exists("Outputs"))
            {
                Directory.CreateDirectory("Outputs");
            }
            fileName += ".csv";
            File.WriteAllText($"Outputs\\{fileName}", sb.ToString(),Encoding.UTF8);
            return fileName;
        }
    }
}
