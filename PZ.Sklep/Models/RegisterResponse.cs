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
    public class RegisterResponse
    {
        [DeserializeAs(Name = "resultCode")]
        public int resultCode { get; set; }
    }
}