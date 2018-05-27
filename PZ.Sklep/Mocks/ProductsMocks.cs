using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PZ.Sklep.Models;

namespace PZ.Sklep.Mocks
{
    public static class ProductsMocks
    {
        public static List<Product> JakiesFejkoweProdukty = new List<Product>()
        {
            // wywaliłam czesc tych mocków bo mi sie nie chciało przerabiać id na string we wsszystkich
            // a i tak beda pobierane te produkty niedługo
            // wincyj produktow do testow
            new Product()
            {
                Id="aaa",
                Name = "produkt1",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 100,
                Description = new ProductDescription()
                {
                    Description = "bla bla opis"
                },
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                },
                Img = "xd"
            },
            new Product()
            {
                Id="bbb",
                Name = "produkt2",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 10,
                Description = new ProductDescription()
                {
                    Description = "bla bla opis22222"
                },
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
                ,
                Img = "muka"
            }
        };
    }
}