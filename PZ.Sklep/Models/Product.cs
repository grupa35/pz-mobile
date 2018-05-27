using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ.Sklep.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductDescription Description { get; set; }
        public List<string> Tags { get; set; }
        public Dictionary<string,int> SizesQuantity { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public Category Category { get; set; }
    }
}