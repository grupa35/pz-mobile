using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using PZ.Sklep.Models;
using Newtonsoft.Json.Linq;
using PZ.Sklep.Mocks;
using System;

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
            //podobno księciunie z bakendu mają uwzględnić kategorie i stronicowanie podczas pobierania produktów ale czy tak będzie to niewiadomo xd
            var request = new RestRequest("/api/products/");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            SessionService.cachedProducts = await DeserializeProducts(response.Content);
        }
        public static async Task DownloadAllProductsFromMock()
        {
            await Task.Run(() => {
                SessionService.cachedProducts = ProductsMocks.JakiesFejkoweProdukty;
            });
        }
        //public static async Task DownloadCategoriesFromAPI()
        //{
        //    var request = new RestRequest("/api/categories/");
        //    IRestResponse response = await client.ExecuteTaskAsync(request);
        //    SessionService.cachedCategories = await DeserializeCategories(response.Content);
        //}

        public static async Task DownloadFromApi<T>(string url)
        {
            var request = new RestRequest(url);
            IRestResponse<T> response = await client.ExecuteTaskAsync<T>(request);
            SessionService.Data[url] = response.Data;
        }

        private static async Task<List<Product>> DeserializeProducts(string json)
        {
            //trzeba sie tak jebać bo bekend ma jakieś popierdolone te obiekty
            List<Product> data = new List<Product>();
            await Task.Run(() => {
                JArray productsJSON = JArray.Parse(json);
                data = productsJSON.Select(p => new Product
                {
                    //jak bekend będzie wysyłał zdjęcia? - chuj wie. reszta danych jest popierdolona to ich nie ustawiam
                    Id = 1,//myślałem że to będzie liczba xd ale bekend wysyła string :<
                    Name = (string)p["name"],
                    Price = (decimal)p["price"],
                    Img = "xd",
                    Tags = new List<string>()
                    {
                        "tag1", "tag2","tag3"
                    },
                    Description = new ProductDescription()
                    {
                        Description = "bla bla opis"
                    },
                    SizesQuantity = new Dictionary<string, int>()
                    {
                        {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                    }
                }).ToList();
            });
            return data;
        }

        //private static async Task<List<Category>> DeserializeCategories(string json)
        //{
        //    List<Category> data = new List<Category>();
        //    await Task.Run(() => {
        //        JArray productsJSON = JArray.Parse(json);
        //        data = productsJSON.Select(p => new Category
        //        {             
        //            id = (string)p["id"],
        //            name = (string)p["name"],
        //            subcategories =
        //            p["subcategories"].Select(c => new Category
        //            {
        //                id = (string)c["id"],
        //                name = (string)c["name"],
        //                // problem jak więcej zagnieżdżonych podkategorii
        //            }).ToList(),
        //        }).ToList();
        //    });
        //    return data;
        //}
    }
}