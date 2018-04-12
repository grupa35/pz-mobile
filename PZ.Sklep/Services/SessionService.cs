using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PZ.Sklep.Models;

namespace PZ.Sklep.Services
{
    public static class SessionService
    {
        private static AppUser user;
        private static List<Product> cachedProducts;
        private static Cart cart;

        static SessionService()
        {
            cachedProducts = new List<Product>();
            cart = new Cart();
        }
    }
}