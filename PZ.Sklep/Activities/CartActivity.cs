using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PZ.Sklep.Models;
using PZ.Sklep.Services;
using PZ.Sklep.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace PZ.Sklep.Activities
{
    [Activity(Label = "CartActivity", Theme = "@style/CategoryTheme")]
    public class CartActivity : Activity
    {
        ListView productList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.CartView);

            productList = FindViewById<ListView>(Resource.Id.cartListView);
            productList.ItemClick += OnProductClickHandler;

            var tittle = FindViewById<TextView>(Resource.Id.cartViewTitle);
            tittle.Text = MenuItemStrings.Cart;

            var totalPrice = FindViewById<TextView>(Resource.Id.cartTotalPrice);
            totalPrice.Text = CountTotalPrice();

            productList.Adapter = new CartListAdapter(SessionService.cart.Products, totalPrice);
        }

        private void OnProductClickHandler(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ProductDetailsActivity));
            intent.PutExtra("sessionProductId", e.Position);
            StartActivity(intent);
        }
        private string CountTotalPrice()
        {
            decimal sum = 0;
            foreach(var x in SessionService.cart.Products)
                sum += x.Price;

            return sum.ToString();
        }
    }
}