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
    [Activity(Label = "ProductDetailsActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class ProductDetailsActivity : Activity
    {
        LinearLayout singleProductView;
        ImageView singleProductPhoto;
        TextView productName;
        TextView productDescription;
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

            Product product = SessionService.cachedProducts.Where(x => x.Id.Equals(productId)).FirstOrDefault();

            //int mydrw = (int)typeof(Resource.Drawable).GetField(product.Img).GetValue(null);
            //singleProductPhoto.SetImageDrawable(this.GetDrawable(mydrw));
            Picasso.With(this).Load(product.Img).Into(singleProductPhoto);
            productName.Text = product.Name;
            productDescription.Text = product.Description.Description;
        }


    }
}