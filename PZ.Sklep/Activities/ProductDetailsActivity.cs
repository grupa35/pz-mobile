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
using PZ.Sklep.Models;
using PZ.Sklep.Services;
using Square.Picasso;

namespace PZ.Sklep.Activities
{
    [Activity(Label = "ProductDetailsActivity", Theme = "@style/CategoryTheme")]
    public class ProductDetailsActivity : Activity
    {
        LinearLayout singleProductView;
        ImageView singleProductPhoto;
        TextView productName;
        TextView productDescription;
        TextView productPrice;
        Button addToCartButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProductDetailsPage);
            string idProduct = Intent.GetStringExtra("sessionProductId");
            SetViewData(idProduct);

            addToCartButton = FindViewById<Button>(Resource.Id.addProductToCartBtn);
            addToCartButton.Click += delegate
            {
                var idPorduct = SessionService.cachedProducts.Where(x => x.Id.Equals(idProduct)).FirstOrDefault();
                SessionService.cart.Products.Add(idPorduct);

                // czemu tutaj tak brzydko wygląda to nie mam pojęcia :c
                AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Dodano produkt");
                alert.SetMessage(idPorduct.Name + " dodany do koszyka.");
                alert.SetButton("Przejdź do koszyka", (c, ev) =>
                {
                    var intent = new Intent(this, typeof(CartActivity));
                    StartActivity(intent);
                });
                alert.SetButton2("Kontynuuj zakupy", (c, ev) => { });
                alert.Show();
            };
        }

        private void SetViewData(string productId)
        {
            singleProductView = FindViewById<LinearLayout>(Resource.Id.singleProductView);
            singleProductPhoto = FindViewById<ImageView>(Resource.Id.productImage);
            productName = FindViewById<TextView>(Resource.Id.productName);
            productDescription = FindViewById<TextView>(Resource.Id.productDescription);
            productPrice = FindViewById<TextView>(Resource.Id.productPrice);

            Product product = SessionService.cachedProducts.Where(x => x.Id.Equals(productId)).FirstOrDefault();

            //int mydrw = (int)typeof(Resource.Drawable).GetField(product.Img).GetValue(null);
            //singleProductPhoto.SetImageDrawable(this.GetDrawable(mydrw));
            Picasso.With(this).Load(product.Img).Resize(600, 800).CenterCrop().Into(singleProductPhoto);
            productName.Text = product.Name;
            productDescription.Text = product.Description.Description;
            productPrice.Text = product.Price.ToString() + " zł";

            //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner2);
            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            //string[] planetNames = { "elo", "elo2" };
            //var adapter = new ArrayAdapter<string>(this, Resource.Layout.spinner_text, planetNames);
            //adapter.SetDropDownViewResource(Resource.Drawable.simple_spinner_dropdown);
            //spinner.Adapter = adapter;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Spinner spinner = (Spinner)sender;
            //string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}