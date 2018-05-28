using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using PZ.Sklep.Services;
using System;

namespace PZ.Sklep
{
    [Activity(Label = "PZ.Sklep", Theme = "@android:style/Theme.NoTitleBar")]
    public class Main : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            Button button = FindViewById<Button>(Resource.Id.btnSignIn);

            if (!SessionService.IsUserLogged)
            {
                button.Text = "Zaloguj się";
                button.Click += delegate {
                    var intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                };
            }
            else
            {
                button.Text = "Wyloguj się";
                button.Click += delegate {
                    SessionService.LogOff();
                    this.Finish();
                    this.OnCreate(null);
                };
            }

            Button button2 = FindViewById<Button>(Resource.Id.btnSignUp);
            button2.Click += delegate {
                var intent = new Intent(this, typeof(PokazSklepyActivity));
                StartActivity(intent);
            };
        }
    }
}
