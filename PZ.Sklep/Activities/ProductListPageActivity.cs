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
using PZ.Sklep.Services;

namespace PZ.Sklep.Activities
{
    [Activity(Label = "ProductListPageActivity")]
    public class ProductListPageActivity: Activity
    {

        ListView productList; 

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ProductListView);

            productList = FindViewById<ListView>(Resource.Id.productListView);
            productList.ItemClick += OnProductClickHandler;

            ProgressDialog progressDialog = UITools.CreateAndShowLoadingDialog(this);

            await RESTService.DownloadProductsFromMock().ContinueWith(t =>
            {
                RunOnUiThread(() =>
                {
                    UITools.EndLoadingDialog(progressDialog);
                });
            });

            productList.Adapter = new MyCustomListAdapter(SessionService.cachedProducts);

        }
        private void OnProductClickHandler(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ProductDetailsActivity));
            intent.PutExtra("sessionProductId", e.Position);
            StartActivity(intent);
        }

    }
}