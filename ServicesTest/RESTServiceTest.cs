using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZ.Sklep.Mocks;
using PZ.Sklep.Services;

namespace ServicesTest
{
    [TestClass]
    public class RESTServiceTest
    {
        [TestMethod]
        public async Task SimpleGET()
        {
            Task<string> task = RESTService.SimpleGET();
            string ret = await task;
            Assert.AreEqual(ret, "hello world", true);
        }

        [TestMethod]
        public async Task DownloadProductsFromAPI()
        {
            Task task = RESTService.DownloadProductsFromAPI();
            await task;
            Assert.AreEqual(SessionService.cachedProducts, ProductsMocks.JakiesFejkoweProdukty);
        }
    }
}
