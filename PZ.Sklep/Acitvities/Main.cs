using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using PZ.Sklep.Services;

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
            button.Click += delegate {
                var intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            };

            Button button2 = FindViewById<Button>(Resource.Id.btnSignUp);
            button2.Click += async delegate {
                var str = RESTService.SimpleGET();
                button2.Text = await str;
            };
        }
    }
}

