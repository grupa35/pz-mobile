using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using PZ.Sklep.Services;
using PZ.Sklep.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace PZ.Sklep.Activities
{
    [Activity(Label = "ProductListPageActivity", Theme = "@style/CategoryTheme")]
    public class ProductListPageActivity: Activity
    {

        ListView productList;
        private int PAGESIZE = 9;
        private ProgressDialog progressDialog;
        private Button btnLoad;
        private List<Product> allItems;
        private List<Product> zmiennaktorarozumiemtylkojahahaha;
        private int page = 1;
        private int maxPosition;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ProductListView);

            allItems = new List<Product>();
            productList = FindViewById<ListView>(Resource.Id.productListView);
            productList.ItemClick += OnProductClickHandler;

            progressDialog = UITools.CreateAndShowLoadingDialog(this);

            await RESTService.DownloadProductsFromAPI().ContinueWith(t =>
            {
                RunOnUiThread(() =>
                {
                    UITools.EndLoadingDialog(progressDialog);
                });
            });
            string categoryId = Intent.GetStringExtra("categoryId");

            zmiennaktorarozumiemtylkojahahaha = SessionService.cachedProducts.Where(x => x.Category.name.Equals(categoryId)).ToList();
            if (zmiennaktorarozumiemtylkojahahaha.Count <= PAGESIZE)
                PAGESIZE = zmiennaktorarozumiemtylkojahahaha.Count;
            //masakra o co tu chodziło...
            allItems = zmiennaktorarozumiemtylkojahahaha.Take(PAGESIZE).ToList();
            maxPosition = allItems.Count;

            btnLoad = new Button(this);
            btnLoad.Text = "Load more";

            if (allItems.Count == 0)
            {
                btnLoad.Enabled = false;
                btnLoad.Text = "Brak przedmiotów!";
            }
            else if (zmiennaktorarozumiemtylkojahahaha.Count <= PAGESIZE)
            {
                btnLoad.Visibility = ViewStates.Invisible;
            }

            btnLoad.Click += BtnLoadMore_ClickAsync;
            productList.AddFooterView(btnLoad);
        
            productList.Adapter = new MyCustomListAdapter(allItems);
        }

        private async void BtnLoadMore_ClickAsync(object sender, EventArgs e)
        {
            progressDialog = UITools.CreateAndShowLoadingDialog(this);

            await Task.Delay(800); // do wyjebania

            RunOnUiThread(() =>
            {
                UITools.EndLoadingDialog(progressDialog);

                var list = zmiennaktorarozumiemtylkojahahaha.Skip(page * PAGESIZE).Take(PAGESIZE).ToList();
                allItems.AddRange(list);
                productList.Adapter = new MyCustomListAdapter(allItems);
                productList.SetSelection(maxPosition);
            });

            if (allItems.Count % 2 != 0)
            {
                btnLoad.Visibility = ViewStates.Invisible;
            }
            else if (allItems.Count % 2 == 0 && allItems.Count == zmiennaktorarozumiemtylkojahahaha.Count)
            {
                btnLoad.Visibility = ViewStates.Invisible;

            }

            maxPosition = allItems.Count;
            page++;
        }

        private void OnProductClickHandler(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ProductDetailsActivity));
            int position = e.Position;
            string id = allItems[position].Id;
            intent.PutExtra("sessionProductId", id);
            StartActivity(intent);
        }

    }
}