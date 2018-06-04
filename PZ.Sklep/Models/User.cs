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
    public class User
    {
        [DeserializeAs(Name = "id")]
        public string id { get; set; }
        [DeserializeAs(Name = "name")]
        public string name { get; set; }
        [DeserializeAs(Name = "surname")]
        public string surname { get; set; }
        [DeserializeAs(Name = "email")]
        public string email { get; set; }
        [DeserializeAs(Name = "role")]
        public Role role { get; set; }
        [DeserializeAs(Name = "addresses")]
        public List<Address> addresses { get; set; }
        [DeserializeAs(Name = "enabled")]
        public bool enabled { get; set; }
        [DeserializeAs(Name = "authorities")]
        public object authorities { get; set; }
        [DeserializeAs(Name = "username")]
        public object username { get; set; }
        [DeserializeAs(Name = "accountNonExpire")]
        public bool accountNonExpired { get; set; }
        [DeserializeAs(Name = "accountNonLocked")]
        public bool accountNonLocked { get; set; }
        [DeserializeAs(Name = "credentialsNonExpired")]
        public bool credentialsNonExpired { get; set; }
    }
}