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
            foreach (string x in lista)
            {
                if (Id.Contains(x))
                    return true;

                if (Name.Contains(x))
                    return true;

                if (Description.Description.Contains(x))
                    return true;

                if(Tags.Any(y => y.Contains(x)) == true)
                    return true;

                if(SizesQuantity.Any(y => y.Equals(x)) == true)
                    return true;

                if (Price == decimal.Parse(x))
                    return true;

                if (Img.Contains(x))
                    return true;
            }

            return false;
        }
    }
}