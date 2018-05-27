using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ.Sklep.Models
{
    public class Cart
    {
        public List<Product> Products { get; set; }
        public Cart()
        {
            Products = new List<Product>();
        }
    }
}