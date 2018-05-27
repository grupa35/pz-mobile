using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using PZ.Sklep.Activities;
using PZ.Sklep.Models;
using PZ.Sklep.Services;
using Square.Picasso;

namespace PZ.Sklep
{
    public class CartListAdapter : BaseAdapter<Product>
    {
        List<Product> products;
        TextView price;

        public CartListAdapter(List<Product> products, TextView price)
        {
            this.products = products;
            this.price = price;
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
            holder.Price.Text = "Cena: " + products[position].Price;

            var localClickListener = new LocalOnclickListener();
            localClickListener.HandleOnClick = () =>
            {
                Toast.MakeText(parent.Context, "Produkt usunięty z koszyka!", ToastLength.Long).Show();
                SessionService.cart.Products.Remove(products[position]);

                this.NotifyDataSetChanged();
                decimal sum = 0;
                foreach (var x in SessionService.cart.Products)
                    sum += x.Price;
                price.Text = sum.ToString();
                //nie wiem czemu nie działa 

            };
            holder.Btn.SetOnClickListener(localClickListener);
            //holder.Btn.SetBackgroundDrawable(Resource.Drawable.removeFromCart);
            holder.Btn.SetBackgroundResource(Resource.Drawable.removeFromCart);

            return view;
        }
    }
}