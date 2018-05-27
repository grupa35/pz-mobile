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

namespace PZ.Sklep
{
    public class ShopListAdapter : BaseAdapter<Shop>
    {
        List<Shop> shops;

        public ShopListAdapter(List<Shop> shops)
        {
            this.shops = shops;
        }

        public override Shop this[int position]
        {
            get
            {
                return shops[position];
            }
        }

        public override int Count
        {
            get
            {
                return shops.Count;
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
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ShopRow, parent, false);

                var name = view.FindViewById<TextView>(Resource.Id.nameShopRow);

                view.Tag = new ShopHolder() { Name = name };
            }

            var holder = (ShopHolder)view.Tag;

            holder.Name.Text = shops[position].name;

            /*var localClickListener = new LocalOnclickListener();
            localClickListener.HandleOnClick = () =>
            {
                SessionService.cart.Products.Add(shops[position]);
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
            holder.Btn.SetOnClickListener(localClickListener);*/

            return view;
        }
    }
}