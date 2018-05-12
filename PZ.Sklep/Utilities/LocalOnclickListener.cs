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

namespace PZ.Sklep
{
    public class LocalOnclickListener : Java.Lang.Object, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            HandleOnClick();
        }
        public System.Action HandleOnClick { get; set; }
    }
}