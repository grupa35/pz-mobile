﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PZ.Sklep.Utilities
{
    public static class BackgroundsDict
    {
        public static Dictionary<string, int> CategoryImages;

        static BackgroundsDict()
        {
            CategoryImages = new Dictionary<string, int>();
            CategoryImages.Add("5b09e7c35011f42f5ecaf807", Resource.Drawable.man);
            CategoryImages.Add("5b09e7c35011f42f5ecaf808", Resource.Drawable.child);
            CategoryImages.Add("5b09e7e85011f42f5ecaf82b", Resource.Drawable.woman);
            CategoryImages.Add("5b09e7e85011f42f5ecaf82c", Resource.Drawable.man);
            CategoryImages.Add("5b09e7e85011f42f5ecaf82d", Resource.Drawable.child);
        }
    }
}