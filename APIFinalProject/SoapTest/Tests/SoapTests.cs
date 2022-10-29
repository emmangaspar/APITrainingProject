using ServiceReference1;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace SoapTest
{
    [TestClass]
    public class SoapTests
    {

        //Global Variable
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient soapTest =
             new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        private List<tCountryCodeAndName> GetCountryCodeAndNameList()
        {
            return soapTest.ListOfCountryNamesByCode();
        }
        [TestMethod]
        private static tCountryCodeAndName GetRandomRecord(List<tCountryCodeAndName> countryList)
        {
            Random rnd = new Random();
            int rndInt = rnd.Next(0, countryList.Count);
            
            var randomCountry = countryList[rndInt];

            return randomCountry;
        }
        [TestMethod]
        public void Test1()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);
            var randomCountryDetails = soapTest.FullCountryInfo(randomCountry.sISOCode);

            Assert.AreEqual(randomCountryDetails.sISOCode, randomCountry.sISOCode);
            Assert.AreEqual(randomCountryDetails.sName, randomCountry.sName);
        }
        [TestMethod]
        public void Test2()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);

            var top5 = countryList.OrderBy(o => o.sISOCode).Take(5);

            foreach (var country in top5)
            {
                Assert.AreEqual(country.sISOCode, country.sISOCode);
            }
        }
    }
}