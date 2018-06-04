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
    [Activity(Label = "RegisterActivity", Theme = "@style/CategoryTheme")]
    public class RegisterActivity : Activity
    {
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterForm);

            var registerBtn = FindViewById<Button>(Resource.Id.btnRegister);
            registerBtn.Click += delegate
            {
                internetConnection();                          
            };
        }

        private async void internetConnection()
        {
            if (UITools.isConnected())
            {
                var name = FindViewById<EditText>(Resource.Id.regEditName);
                var surname = FindViewById<EditText>(Resource.Id.regEditSurname);
                var mail = FindViewById<EditText>(Resource.Id.regEditEmail);
                var pass = FindViewById<EditText>(Resource.Id.regEditPass);
                var passrep = FindViewById<EditText>(Resource.Id.regEditPassRep);

                string json = $"{{\"password\":\"{pass.Text}\",\"surname\":\"{surname.Text}\",\"rePassword\":\"{passrep.Text}\",\"name\":\"{name.Text}\",\"roleName\":\"customer\",\"email\":\"{mail.Text}\"}}";

                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);
                //RESTService.Register(APIUrlsMap.Register, json);

                await RESTService.DownloadFromApi<RegisterResponse>(APIUrlsMap.Register, json, false, RestSharp.Method.POST).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    });
                });

                var data = (RegisterResponse)SessionService.Data[APIUrlsMap.Register];
                
                if (data.resultCode == 0)
                {
                    Toast.MakeText(this, "Account created", ToastLength.Short).Show();                   
                }
                else
                {
                    Toast.MakeText(this, "Error, try again", ToastLength.Short).Show();
                }
            }
            else
            {
                UITools.checkInternetConnection(this);
                Toast.MakeText(ApplicationContext, "Aplikacja wymaga uzycia internetu !", ToastLength.Short).Show();
            }
        }
    }
}