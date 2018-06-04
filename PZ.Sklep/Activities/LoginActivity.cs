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
using PZ.Sklep.Models;
using PZ.Sklep.Services;
using PZ.Sklep.Utilities;

namespace PZ.Sklep
{
    [Activity(Label = "LoginActivity", Theme = "@style/CategoryTheme")]
    public class LoginActivity : Activity
    {
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginForm);

            var signInBtn = FindViewById<Button>(Resource.Id.btnLogIn);
            signInBtn.Click += delegate
            {
                internetConnection();        
            };

            var registerBtn = FindViewById<Button>(Resource.Id.registerButton);
            registerBtn.Click += delegate
            {
                var intent = new Intent(this, typeof(RegisterActivity));
                StartActivity(intent);
            };

            var withoutAccountBtn = FindViewById<Button>(Resource.Id.withoutAccountButton);
            withoutAccountBtn.Click += delegate
            {
                var intent = new Intent(this, typeof(ShopPageActivity));
                StartActivity(intent);
                Finish();
            };
        }

        private async void internetConnection()
        {
            if (UITools.isConnected())
            {
                var email = FindViewById<EditText>(Resource.Id.editLogin);
                var pass = FindViewById<EditText>(Resource.Id.editPass);
                string json = $"{{\"email\":\"{email.Text}\",\"password\":\"{pass.Text}\"}}";

                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);
                await RESTService.Login(APIUrlsMap.Login, json, false, RestSharp.Method.POST).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    });
                    if (SessionService.Token != string.Empty)
                    {
                        Intent intent = new Intent(this, typeof(ShopPageActivity));
                        StartActivity(intent);
                        Finish();
                    }
                    else // nie wiem czemu bez tego przechodzi do innego activity
                    {
                        Intent intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                        Finish();
                    }
                }); 
            }
            else
            {
                UITools.checkInternetConnection(this);
                Toast.MakeText(ApplicationContext, "Aplikacja wymaga uzycia internetu !", ToastLength.Short).Show();
            }
        }
    }
}