using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PZ.Sklep.Mocks;
using PZ.Sklep.Models;
using PZ.Sklep.Utilities;

namespace PZ.Sklep.Services
{
    public static class SessionService
    {
        public static AppUser user;
        public static List<Product> cachedProducts;
        public static List<Category> cachedCategories;
        public static Cart cart;

        public static Dictionary<string, object> Data;

        static SessionService()
        {
            cachedProducts = new List<Product>();
            cachedCategories = new List<Category>();
            cart = new Cart();
            cart.Products.Add(ProductsMocks.JakiesFejkoweProdukty[1]);
            cart.Products.Add(ProductsMocks.JakiesFejkoweProdukty[4]);
            Data = new Dictionary<string, object>();
            Data.Add(APIUrlsMap.Categories, new List<Category>());
        }
    }
}