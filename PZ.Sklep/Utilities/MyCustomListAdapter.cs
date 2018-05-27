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
using PZ.Sklep.Services;
using Square.Picasso;

namespace PZ.Sklep
{
    public class MyCustomListAdapter : BaseAdapter<Product>
    {
        List<Product> products;

        public MyCustomListAdapter(List<Product> products)
        {
            this.products = products;
        }

        public override Product this[int position]
        {
            get
            {
                return products[position];
            }
        }

        public override int Count
        {
            get
            {
                return products.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.userRow, parent, false);

                var photo = view.FindViewById<ImageView>(Resource.Id.photoImageView);
                var name = view.FindViewById<TextView>(Resource.Id.nameTextView);
                var price = view.FindViewById<TextView>(Resource.Id.departmentTextView);
                var btn = view.FindViewById<Button>(Resource.Id.addProductToCartFromListViewBtn);

                view.Tag = new ViewHolder() { Photo = photo, Name = name, Price = price, Btn = btn };
            }

            var holder = (ViewHolder)view.Tag;

            //holder.Photo.SetImageDrawable(ImageManager.Get(parent.Context, users[position].ImageUrl));
            //int mydrw = (int)typeof(Resource.Drawable).GetField(products[position].Img).GetValue(null);
            //holder.Photo.SetImageDrawable(parent.Context.GetDrawable(mydrw));
            Picasso.With(parent.Context).Load(products[position].Img).Into(holder.Photo);
            holder.Name.Text = products[position].Name;
            holder.Price.Text ="Cena: " + products[position].Price;

            var localClickListener = new LocalOnclickListener();
            localClickListener.HandleOnClick = () =>
            {
                SessionService.cart.Products.Add(products[position]);
                Toast.MakeText(parent.Context, "Produkt dodany do koszyka!", ToastLength.Long).Show();
                //var intent = new Intent(parent.Context, typeof(CartActivity)); 
                //parent.Context.StartActivity(intent);

                //Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(parent.Context);
                //AlertDialog alert = dialog.Create();
                //alert.SetTitle("Add to cart");
                //alert.SetMessage(products[position].Name);
                //alert.SetButton("OK", (c, ev) => { });
                //alert.Show();
            };
            holder.Btn.SetOnClickListener(localClickListener);

            return view;
        }
    }
}