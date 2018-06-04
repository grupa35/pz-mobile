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
using RestSharp.Deserializers;

namespace PZ.Sklep.Models
{
    public class ResponseStatusDTO
    {
        [DeserializeAs(Name = "code")]
        public int code { get; set; }
        [DeserializeAs(Name = "errorMessage")]
        public string errorMessage { get; set; }
    }
}