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

namespace PZ.Sklep.Utilities
{
    public class CardAdapter : BaseAdapter<Category>
    {
        private readonly Activity context;
        private readonly List<Category> cards;

        public CardAdapter(Activity context, List<Category> cards)
        {
            this.context = context;
            this.cards = cards;
        }

        public override Category this[int position]
        {
            get
            {
                return cards[position];
            }
        }

        public override int Count
        {
            get
            {
                return cards.Count;
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
                view = context.LayoutInflater.Inflate(Resource.Layout.CardRow, parent, false);
            }

            var titleTextView = view.FindViewById<TextView>(Resource.Id.cardViewText);
            var background = view.FindViewById<ImageView>(Resource.Id.cardBackground);

            var key = cards[position].id;
            background.SetImageResource(BackgroundsDict.CategoryImages[key]);
            titleTextView.Text = cards[position].name;
            return view;
        }
    }
}