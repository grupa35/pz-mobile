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
            new Product()
            {
                Id=1,
                Name = "produkt1",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 100,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=2,
                Name = "produkt2",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 10,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=3,
                Name = "produkt3",
                Tags = new List<string>()
                {
                    "tag2","tag3"
                },
                Price = 300,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=4,
                Name = "produkt4",
                Tags = new List<string>()
                {
                    "tag3"
                },
                Price = 600,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=5,
                Name = "produkt5",
                Tags = new List<string>()
                {
                    "tag2","tag5"
                },
                Price = 50,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=6,
                Name = "produkt6",
                Tags = new List<string>()
                {
                    "tag1","tag3"
                },
                Price = 60,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=7,
                Name = "produkt7",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 100,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=8,
                Name = "produkt8",
                Tags = new List<string>()
                {
                    "tag4", "tag2"
                },
                Price = 100,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=9,
                Name = "produkt9",
                Tags = new List<string>()
                {
                    "tag1"
                },
                Price = 200,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=10,
                Name = "produkt10",
                Tags = new List<string>()
                {
                    "tag1",
                },
                Price = 10,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=11,
                Name = "produkt11",
                Tags = new List<string>()
                {
                    "tag2"
                },
                Price = 15,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=12,
                Name = "produkt12",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 20,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            },
            new Product()
            {
                Id=13,
                Name = "produkt13",
                Tags = new List<string>()
                {
                    "tag1", "tag2","tag3"
                },
                Price = 69,
                Description = new ProductDescription(),
                SizesQuantity = new Dictionary<string, int>()
                {
                    {"s",10 },{"m",10 },{"l",10 },{"xl",10 }
                }
            }
        };
    }
}