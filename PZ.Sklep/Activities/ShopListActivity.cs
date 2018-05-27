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

namespace PZ.Sklep
{
    [Activity(Label = "PokazSklepyActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class PokazSklepyActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShopListView);

            var listView = FindViewById<ListView>(Resource.Id.listView1);
            var data = new List<Shop>() {
               new Shop("1", "Kiełbaski Janusz"), new Shop("2", "Sklep Andrzeja Maliny"), new Shop("3", "JanuszPol S.A.")
            };
            listView.Adapter = new ShopListAdapter(data);
            listView.ItemClick += onClickFunc;
        }
        private void onClickFunc(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ShopPageActivity));
            StartActivity(intent);
        }
        //private void OnSklepKLIK(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    var data = new string[] {
        //       "Kiełbaski Janusz", "Sklep Andrzeja Maliny", "JanuszPol S.A."
        //    };
        //    string sklep = data[e.Position];

        //    AlertDialog.Builder alert = new AlertDialog.Builder(this);
        //    alert.SetTitle("dupa?");
        //    alert.SetMessage("xdxdxdxd");
        //    alert.SetPositiveButton(sklep, (senderAlert, args) => {
        //        Toast.MakeText(this, "elo", ToastLength.Short).Show();
        //    });

        //    alert.SetNegativeButton("szuster", (senderAlert, args) => {
        //        Toast.MakeText(this, "plichta xD", ToastLength.Short).Show();
        //    });

        //    Dialog dialog = alert.Create();
        //    dialog.Show();
        //}
    }
}