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
    class CategoryListViewAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<Category> listGroup;

        public CategoryListViewAdapter(Context context, List<Category> listGroup) // string
        {
            this.context = context;
            this.listGroup = listGroup;
        }

        public override int GroupCount => listGroup.Count;

        public override bool HasStableIds => false;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return listGroup[groupPosition].subcategories[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return listGroup[groupPosition].subcategories.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.SubcategoryRow, null);
                var name = convertView.FindViewById<TextView>(Resource.Id.subcategoryRowName);
                convertView.Tag = new CategoryHolder() { Name = name };
            }
            var holder = (CategoryHolder)convertView.Tag;
            holder.Name.Text = listGroup[groupPosition].subcategories[childPosition].name; //--

            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listGroup[groupPosition].name;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.CategoryRow, null);
                var name = convertView.FindViewById<TextView>(Resource.Id.categoryRowName);
                convertView.Tag = new CategoryHolder() { Name = name };
            }
            var holder = (CategoryHolder)convertView.Tag;
            holder.Name.Text = listGroup[groupPosition].name;

            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}