using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PZ.Sklep.Services
{
    public static class RESTService
    {
        private static RestClient client;
        static RESTService()
        {
            client = new RestClient("http://shopgen.pl");
        }
        public static async Task<string> SimpleGET()
        {
            string msg = string.Empty;
            var request = new RestRequest("/api/test/hello");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            return msg = response.Content;
        }
    }
}