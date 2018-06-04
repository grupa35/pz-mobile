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
    [Activity(Label = "ProfileActivity", Theme = "@style/CategoryTheme")]
    public class ProfileActivity : Activity
    {
        ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileForm);

            internetConnection();

            var resetPassBtn = FindViewById<Button>(Resource.Id.profileReset);
            resetPassBtn.Click += delegate
            {
                resetPasswordConnection();
                /*var intent = new Intent(this, typeof(ProfileActivity));
                StartActivity(intent);
                Finish();*/
            };

            var changeEmailBtn = FindViewById<Button>(Resource.Id.profileConfirm);
            changeEmailBtn.Click += delegate
            {
                changeEmailConnection();                             
            };
        }

        private async void internetConnection()
        {
            if (UITools.isConnected())
            {
                var nameTxt = FindViewById<TextView>(Resource.Id.profileName);
                var surnameTxt = FindViewById<TextView>(Resource.Id.profileSurname);
                var emailTxt = FindViewById<TextView>(Resource.Id.profileEmail);
                var roleTxt = FindViewById<TextView>(Resource.Id.profileRole);

                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);

                await RESTService.DownloadFromApi<User>(APIUrlsMap.CurrentUser, null, true).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    });
                });

                var data = (User)SessionService.Data[APIUrlsMap.CurrentUser];
                nameTxt.Text = data.name;
                surnameTxt.Text = data.surname;
                emailTxt.Text = data.email;
                roleTxt.Text = data.role.name;
            }
            else
            {
                UITools.checkInternetConnection(this);
                Toast.MakeText(ApplicationContext, "Aplikacja wymaga uzycia internetu !", ToastLength.Short).Show();
            }
        }

        private async void resetPasswordConnection()
        {
            if (UITools.isConnected())
            {
                var passold = FindViewById<EditText>(Resource.Id.profileOldPass);
                var pass = FindViewById<EditText>(Resource.Id.profileNewPass);
                var passrep = FindViewById<EditText>(Resource.Id.profileNewPassRep);
                string json = $"{{\"oldPassword\":\"{passold.Text}\",\"newPassword\":\"{pass.Text}\",\"reNewPassword\":\"{passrep.Text}\"}}";

                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);
                await RESTService.DownloadFromApi<ResponseStatusDTO>(APIUrlsMap.ResetPassword, json, true, RestSharp.Method.POST).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    });
                });

                var data = (ResponseStatusDTO)SessionService.Data[APIUrlsMap.ResetPassword];
                if (data.code == 0)
                {
                    Toast.MakeText(this, "Password changed", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "error", ToastLength.Short).Show();
                }
            }
            else
            {
                UITools.checkInternetConnection(this);
                Toast.MakeText(ApplicationContext, "Aplikacja wymaga uzycia internetu !", ToastLength.Short).Show();
            }
        }

        private async void changeEmailConnection()
        {
            if (UITools.isConnected())
            {
                var email = FindViewById<EditText>(Resource.Id.profileNewMail);
                string json = $"{{\"email\":\"{email.Text}\"}}";

                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);
                await RESTService.DownloadFromApi<ResponseStatusDTO>(APIUrlsMap.ChangeMail, json, true, RestSharp.Method.POST).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    });
                });

                var data = (ResponseStatusDTO)SessionService.Data[APIUrlsMap.ChangeMail];
                if (data.code == 0)
                {
                    RESTService.Logout(APIUrlsMap.Logout);
                    var intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "error", ToastLength.Short).Show();
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