using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PZ.Sklep.Models;

namespace PZ.Sklep.Services
{
    public static class SessionService
    {
        public static AppUser user;
        public static List<Product> cachedProducts;
        public static Cart cart;

        static SessionService()
        {
            cachedProducts = new List<Product>();
            cart = new Cart();
        }
    }
}