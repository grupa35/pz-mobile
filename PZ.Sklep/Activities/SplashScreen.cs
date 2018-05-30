using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PZ.Sklep
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/CategoryTheme")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Splash);            
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { DoSplash(); });
            startupWork.Start();
        }
        async void DoSplash()
        {
            await Task.Delay(1500);//<--- musi być ta liczba, inna nie działa
            StartActivity(new Intent(Application.Context, typeof(LoginActivity)));
        }
    }
}