using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PZ.Sklep.Models
{
    public class ProductDescription
    {
        public ProductDescription()
        {
            Description = "pusty opis produktu";
            Value = "waaaaat?";
        }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}