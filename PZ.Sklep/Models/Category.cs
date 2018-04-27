using System.Collections.Generic;

namespace PZ.Sklep.Models
{
    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Category> subcategories { get; set; }//kompletnie nieogarniam po chuj to no ale dobra, księciunie z bekendu tak chcą
    }
}