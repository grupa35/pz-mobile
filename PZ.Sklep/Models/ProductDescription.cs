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
        }
        public string Description { get; set; }
    }
}