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
using PZ.Sklep.Activities;

namespace PZ.Sklep
{
    [Activity(Label = "LoginActivity", Theme = "@style/CategoryTheme")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginForm);

            var withoutAccountBtn = FindViewById<Button>(Resource.Id.withoutAccountButton);
            withoutAccountBtn.Click += delegate
            {
                var intent = new Intent(this, typeof(ShopPageActivity));
                StartActivity(intent);
                Finish();
            };
        }
    }
}