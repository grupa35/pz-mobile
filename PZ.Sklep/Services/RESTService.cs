using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using PZ.Sklep.Models;
using Newtonsoft.Json.Linq;
using PZ.Sklep.Mocks;
using System;
using RestSharp.Deserializers;
using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace PZ.Sklep.Services
{
    public static class RESTService
    {
        private static RestClient client;
        static RESTService()
        {
            client = new RestClient("http://shopgen.pl");
        }
        public static async Task DownloadProductsFromAPI(string category = "")
        {
            var request = new RestRequest("/api/products/");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            SessionService.cachedProducts = await DeserializeProducts(response.Content);
        }
        //public static async Task DownloadAllProductsFromMock()
        //{
        //    await Task.Run(() => {
        //        SessionService.cachedProducts = ProductsMocks.JakiesFejkoweProdukty;
        //    });
        //}
        //public static async Task DownloadCategoriesFromAPI()
        //{
        //    var request = new RestRequest("/api/categories/");
        //    IRestResponse response = await client.ExecuteTaskAsync(request);
        //    SessionService.cachedCategories = await DeserializeCategories(response.Content);
        //}

        public static async Task DownloadFromApi<T>(string url, string strJSONContent = null, bool auth = false, Method method = Method.GET)
        {
            var request = new RestRequest(url, method);
            var tok = SessionService.Token;
            if (strJSONContent != null)
            {
                request.Parameters.Clear();
                request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);
            }
            if (auth)
                request.AddHeader("Authorization", "Bearer " + SessionService.Token);     
            IRestResponse<T> response = await client.ExecuteTaskAsync<T>(request);
            SessionService.Data[url] = response.Data;
        }

        public static async Task Login(string url, string strJSONContent = null, bool auth = false, Method method = Method.GET)
        {
            var request = new RestRequest(url, method);
            var tok = SessionService.Token;
            if (strJSONContent != null)
            {
                request.Parameters.Clear();
                request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);
            }
            if (auth)
                request.AddHeader("Authorization", "Bearer " + SessionService.Token);
            IRestResponse<LoginResponse> response = await client.ExecuteTaskAsync<LoginResponse>(request);
            SessionService.Data[url] = response.Data;
            var status = response.StatusCode.ToString();
            if (status == "OK")
                SessionService.Token = response.Headers[0].Value.ToString();
        }

        public static string Logout(string url)
        {
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", "Bearer " + SessionService.Token);
            IRestResponse response = client.Execute(request);
            var status = response.StatusCode.ToString();
            if (status == "NoContent")
                SessionService.Token = string.Empty;   
            return status;
        }

        private static async Task<List<Product>> DeserializeProducts(string json)
        {
            //trzeba sie tak jebać bo bekend ma jakieś popierdolone te obiekty
            List<Product> data = new List<Product>();
            await Task.Run(() => {
                JArray productsJSON = JArray.Parse(json);
                data = productsJSON.Select(p => new Product
                {
                    Id = (string)p["id"],
                    Name = (string)p["name"],
                    Price = (decimal)p["price"],
                    Img = "http://shopgen.pl" + (string)p["imgUrl"],
                    Category = new Func<JToken, Category>(jt => {
                        var jCategory = jt.SelectToken("category");
                        if (jCategory.HasValues == false)
                            return new Category() { id = "api", name = "srapi" };
                        string id = (string)jCategory["id"];
                        string name = (string)jCategory["name"];
                        return new Category()
                        {
                            id = id,
                            name = name
                        };
                    })(p),
                    Tags = new List<string>()
                    {
                        "tag1", "tag2","tag3"
                    },
                    Description = new Func<JToken, ProductDescription>(jt => {
                        var jDescription = jt.SelectToken("description");
                        if (jDescription.HasValues == false)
                            return new ProductDescription() { Description = "xd" };
                        string id = (string)jDescription["id"];
                        string name = (string)jDescription["name"];
                        string value = (string)jDescription["value"];
                        return new ProductDescription()
                        {
                            Description = value,
                        };
                    })(p),
                    SizesQuantity = new Dictionary<string, int>()
                    {
                        {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                    }
                }).ToList();
            });
            return data;
        }
    }
}