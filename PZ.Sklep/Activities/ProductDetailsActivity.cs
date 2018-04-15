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
    [Activity(Label = "ProductDetailsActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class ProductDetailsActivity : Activity
    {
        LinearLayout singleProductView;
        ImageView singleProductPhoto;
        TextView productName;
        TextView productDescription;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProductDetailsPage);
            SetViewData(Intent.GetIntExtra("sessionProductId",0));
        }
        private void SetViewData(int productId)
        {
            singleProductView = FindViewById<LinearLayout>(Resource.Id.singleProductView);
            singleProductPhoto = FindViewById<ImageView>(Resource.Id.productImage);
            productName = FindViewById<TextView>(Resource.Id.productName);
            productDescription = FindViewById<TextView>(Resource.Id.productDescription);

            int mydrw = (int)typeof(Resource.Drawable).GetField(SessionService.cachedProducts[productId].Img).GetValue(null);
            singleProductPhoto.SetImageDrawable(this.GetDrawable(mydrw));
            productName.Text = SessionService.cachedProducts[productId].Name;
            productDescription.Text = SessionService.cachedProducts[productId].Description.Description;
        }
    }
}