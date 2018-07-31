using AddressInfo.API.Contracts;
using AddressInfo.API.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace AddressInfo.Test
{
    [TestClass]
    public class AdressInfoUnitTests
    {

        IDataGenerator<API.Model.AddressInfo> _dataGenerator;
        string xmlPath = "Data\\sample_data.xml";
        string csvPath = "Data\\sample_data.csv";
        /// <summary>
        ///  Generate XML output from CSV input, filtered by City name=’Antalya’ 
        /// </summary>
        [TestMethod]
        public void Case1()
        {
            _dataGenerator = new AddressInfoCsvGenerator();
            var data = _dataGenerator.Generate<API.Model.AddressInfo>(csvPath,query: x => x.Name == "Antalya");
            _dataGenerator = new AddressInfoXmlGenerator();
            Assert.IsNotNull(_dataGenerator.Save(data), "baþarýlý bir þekilde kaydedildi");

        }
        /// <summary>
        /// Generate CSV output from CSV input, sorted by City name ascending, then District name ascending
        /// </summary>
        [TestMethod]
        public void Case2()
        {
            _dataGenerator = new AddressInfoCsvGenerator();
            var data = _dataGenerator.Generate(csvPath);
            data.Cities = data.Cities.OrderBy(x=>x.Name).ThenBy(x=>x.Districts.OrderBy(c=>c.Name)).ToList();
            Assert.IsNotNull(_dataGenerator.Save(data),"baþarýlý");

        }
        /// <summary>
        /// Generate CSV output from XML input, filtered by City name=’Ankara’ and sorted by Zip code descending 
        /// </summary>
        [TestMethod]
        public void Case3()
        {
            _dataGenerator = new AddressInfoXmlGenerator();
            var data = _dataGenerator.Generate(xmlPath, x => x.Name == "Ankara", c => c.Districts.OrderBy(v => v.Zips.OrderByDescending(y => y.Code)));
            _dataGenerator = new AddressInfoCsvGenerator();
            Assert.IsNotNull(_dataGenerator.Save(data), "baþarýlý bir þekilde kaydedildi");


        }
    }
}
