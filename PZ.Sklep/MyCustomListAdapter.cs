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
                var description = view.FindViewById<TextView>(Resource.Id.departmentTextView);

                view.Tag = new ViewHolder() { Photo = photo, Name = name, Description = description };
            }

            var holder = (ViewHolder)view.Tag;

            //holder.Photo.SetImageDrawable(ImageManager.Get(parent.Context, users[position].ImageUrl));
            int mydrw = (int)typeof(Resource.Drawable).GetField(products[position].Img).GetValue(null);
            holder.Photo.SetImageDrawable(parent.Context.GetDrawable(mydrw));
            holder.Name.Text = products[position].Name;
            holder.Description.Text = products[position].Description.Description;
            return view;
        }
    }
}