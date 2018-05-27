using System.Collections.Generic;

namespace PZ.Sklep.Models
{
    public class Shop : Java.Lang.Object
    {
        public Shop()
        {}

        public Shop(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string id { get; set; }
        public string name { get; set; }
    }
}