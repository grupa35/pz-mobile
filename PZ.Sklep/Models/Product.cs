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

        public bool CheckIfContains(List<string> lista)
        {
            decimal g = 0;
            foreach (string x in lista)
            {
                if (Id.Contains(x))
                    return true;

                if (Name.ToLower().Contains(x))
                    return true;

                if (Description.Description.ToLower().Contains(x))
                    return true;

                if(Tags.Any(y => y.ToLower().Contains(x)) == true)
                    return true;

                if(SizesQuantity.Any(y => y.Equals(x)) == true)
                    return true;

                if (decimal.TryParse(x, out g) || g.Equals(Price))
                    return true;
        
                if (Img.Contains(x))
                    return true;
            }

            return false;
        }
    }
}