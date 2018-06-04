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

namespace PZ.Sklep.Models
{
    public class Address
    {
        public string id { get; set; }
        public string addressName { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string postalNumber { get; set; }
        public string postalCity { get; set; }
        public string details { get; set; }
    }
}