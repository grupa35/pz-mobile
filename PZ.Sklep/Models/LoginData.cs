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
    class LoginRelease
    {
        [DeserializeAs(Name = "resultCode")]
        public int ResultCode { get; set; }
        [DeserializeAs(Name = "token")]
        public string Token { get; set; }
    }
}