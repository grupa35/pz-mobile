﻿using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.Animations;
using System.Linq;
using Android.Graphics;
using PZ.Sklep.Services;
using PZ.Sklep.Utilities;
using PZ.Sklep.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PZ.Sklep.Activities
{
    [Activity(Label = "SlidingMenu", Theme = "@style/CategoryTheme")]
    public class ShopPageActivity : Activity
    {
        GestureDetector gestureDetector;
        GestureListener gestureListener;

        ListView menuListView;
        MenuListAdapterClass objAdapterMenu;
        ImageView menuIconImageView;
        int intDisplayWidth;
        bool isSingleTapFired = false;
        TextView txtActionBarText;
        TextView txtPageName;
        TextView txtDescription;
        ImageView btnDescExpander;
        //ListView myList;
        ExpandableListView categoryListView;
        ProgressDialog progressDialog;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.ShopMainPage);

            FnInitialization();
            TapEvent();
            FnBindMenu();

            txtPageName.Visibility = ViewStates.Invisible;
            
            categoryListView = FindViewById<ExpandableListView>(Resource.Id.myExpandableListview);

            internetConnection();

            //myList = FindViewById<ListView>(Resource.Id.productsMainPageListView);
            //myList.ItemClick += onItemClickFunc;
            //await RESTService.DownloadProductsFromAPI();//wywalic to stąd później
            //myList.Adapter = new MyCustomListAdapter(SessionService.cachedProducts);
        }

        private async void internetConnection()
        {

            if (UITools.isConnected())
            {
                UITools.checkInternetConnection(this);
                progressDialog = UITools.CreateAndShowLoadingDialog(this);
                await RESTService.DownloadFromApi<List<Category>>(APIUrlsMap.Categories).ContinueWith(t =>
                {
                    RunOnUiThread(() =>
                    {
                        UITools.EndLoadingDialog(progressDialog);
                    }); 
                });

                categoryListView.SetAdapter(new CategoryListViewAdapter(this, SessionService.Data[APIUrlsMap.Categories] as List<Category>));
                categoryListView.ChildClick += OnSubcategoryClickHandler;
            }
            else
            {
                UITools.checkInternetConnection(this);
                Toast.MakeText(ApplicationContext, "Aplikacja wymaga uzycia internetu !", ToastLength.Short).Show();
            }
        }

        private void OnSubcategoryClickHandler(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            var intent = new Intent(this, typeof(ProductListPageActivity));
            StartActivity(intent);
        }

        //public override void OnBackPressed()
        //{
        //    txtPageName.Text = "Strona główna";
        //    singleProductView.Visibility = ViewStates.Invisible;
        //    txtPageName.Visibility = ViewStates.Invisible;
        //    myList.Visibility = ViewStates.Visible;
        //}

        /*private void onItemClickFunc(object sender, AdapterView.ItemClickEventArgs e)
        {
            //int mydrw = (int)typeof(Resource.Drawable).GetField(SessionService.cachedProducts[e.Position].Img).GetValue(null);
            //singleProductPhoto.SetImageDrawable(this.GetDrawable(mydrw));
            //productName.Text = SessionService.cachedProducts[e.Position].Name;
            //productDescription.Text = SessionService.cachedProducts[e.Position].Description.Description;
            //myList.Visibility = ViewStates.Invisible;
            //singleProductView.Visibility = ViewStates.Visible;
            //txtPageName.Visibility = ViewStates.Visible;
            var intent = new Intent(this, typeof(ProductDetailsActivity));
            intent.PutExtra("sessionProductId", e.Position);
            StartActivity(intent);
        }*/
        void TapEvent()
        {
            menuIconImageView.Click += delegate (object sender, EventArgs e)
            {
                if (!isSingleTapFired)
                {
                    FnToggleMenu();
                    isSingleTapFired = false;
                }
            };
            //btnDescExpander.Click += delegate (object sender, EventArgs e)
            //{
            //    FnDescriptionWindowToggle();
            //};
        }
        void FnInitialization()
        {
            //gesture initialization
            gestureListener = new GestureListener();
            gestureListener.LeftEvent += GestureLeft;
            gestureListener.RightEvent += GestureRight;
            gestureListener.SingleTapEvent += SingleTap;
            gestureDetector = new GestureDetector(this, gestureListener);

            menuListView = FindViewById<ListView>(Resource.Id.menuListView);
            menuIconImageView = FindViewById<ImageView>(Resource.Id.menuIconImgView);
            txtActionBarText = FindViewById<TextView>(Resource.Id.txtActionBarText);
            txtPageName = FindViewById<TextView>(Resource.Id.txtPage);
            //txtDescription = FindViewById<TextView>(Resource.Id.txtDescription);
            //btnDescExpander = FindViewById<ImageView>(Resource.Id.btnImgExpander);
            //changed sliding menu width to 1/3 of screen width 
            Display display = this.WindowManager.DefaultDisplay;
            var point = new Point();
            display.GetSize(point);
            intDisplayWidth = point.X;
            intDisplayWidth = intDisplayWidth - (intDisplayWidth / 3);
            using (var layoutParams = (RelativeLayout.LayoutParams)menuListView.LayoutParameters)
            {
                layoutParams.Width = intDisplayWidth;
                layoutParams.Height = ViewGroup.LayoutParams.MatchParent;
                menuListView.LayoutParameters = layoutParams;
            }
            //menuListView.LayoutParameters = new RelativeLayout.LayoutParams(intDisplayWidth, ViewGroup.LayoutParams.MatchParent);
        }
        #region " Menu related"
        void FnBindMenu()
        {
            string[] strMnuText = { MenuItemStrings.MainPage, MenuItemStrings.Cart, MenuItemStrings.ShopsList, MenuItemStrings.TurnOff };
            int[] strMnuUrl = { Resource.Drawable.icon_home, Resource.Drawable.cart, Resource.Drawable.list, Resource.Drawable.turn_off };
            if (objAdapterMenu != null)
            {
                objAdapterMenu.actionMenuSelected -= FnMenuSelected;
                objAdapterMenu = null;
            }
            objAdapterMenu = new MenuListAdapterClass(this, strMnuText, strMnuUrl);
            objAdapterMenu.actionMenuSelected += FnMenuSelected;
            menuListView.Adapter = objAdapterMenu;
        }
        void FnMenuSelected(string strMenuText)
        {
            //txtActionBarText.Text = strMenuText;
            //txtPageName.Text = strMenuText;
            if (strMenuText.Equals(MenuItemStrings.Cart))
            {
                ShowActivity(typeof(CartActivity));
            }
            else if (strMenuText.Equals(MenuItemStrings.ShopsList))
            {
                ShowActivity(typeof(PokazSklepyActivity));
            }
            else if (strMenuText.Equals(MenuItemStrings.TurnOff))
            {
                System.Environment.Exit(0);
            }

        }

        private void ShowActivity(Type activity)
        {
            StartActivity(activity);
        }

        void FnToggleMenu()
        {
            if (menuListView.IsShown)
            {
                menuListView.Animation = new TranslateAnimation(0f, -menuListView.MeasuredWidth, 0f, 0f);
                menuListView.Animation.Duration = 300;
                menuListView.Visibility = ViewStates.Gone;
            }
            else
            {
                menuListView.Visibility = ViewStates.Visible;
                menuListView.RequestFocus();
                menuListView.Animation = new TranslateAnimation(-menuListView.MeasuredWidth, 0f, 0f, 0f);//starting edge of layout 
                menuListView.Animation.Duration = 300;
            }
        }
        #endregion

        #region "Gesture function "
        void GestureLeft()
        {
            if (!menuListView.IsShown)
                FnToggleMenu();
            isSingleTapFired = false;
        }
        void GestureRight()
        {
            if (menuListView.IsShown)
                FnToggleMenu();
            isSingleTapFired = false;
        }
        void SingleTap()
        {
            if (menuListView.IsShown)
            {
                FnToggleMenu();
                isSingleTapFired = true;
            }
            else
            {
                isSingleTapFired = false;
            }
        }
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            gestureDetector.OnTouchEvent(ev);
            return base.DispatchTouchEvent(ev);
        }
        #endregion

        #region "Description toggle window"
        //void FnDescriptionWindowToggle()
        //{
        //    if (txtDescription.IsShown)
        //    {
        //        txtDescription.Visibility = ViewStates.Gone;
        //        txtDescription.Animation = new TranslateAnimation(0f, 0f, 0f, txtDescription.MeasuredHeight);
        //        txtDescription.Animation.Duration = 300;
        //        btnDescExpander.SetImageResource(Resource.Drawable.up_arrow);
        //    }
        //    else
        //    {
        //        txtDescription.Visibility = ViewStates.Visible;
        //        txtDescription.RequestFocus();
        //        txtDescription.Animation = new TranslateAnimation(0f, 0f, txtDescription.MeasuredHeight, 0f);
        //        txtDescription.Animation.Duration = 300;
        //        btnDescExpander.SetImageResource(Resource.Drawable.down_arrow);
        //    }
        //}
        #endregion
    }

    #region " Menu list adapter"
    public class MenuListAdapterClass : BaseAdapter<string>
    {
        Activity _context;
        string[] _mnuText;
        int[] _mnuUrl;
        internal event Action<string> actionMenuSelected;
        public MenuListAdapterClass(Activity context, string[] strMnu, int[] intImage)
        {
            _context = context;
            _mnuText = strMnu;
            _mnuUrl = intImage;
        }
        public override string this[int position]
        {
            get { return this._mnuText[position]; }
        }

        public override int Count
        {
            get { return this._mnuText.Count(); }
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            MenuListViewHolderClass objMenuListViewHolderClass;
            View view;
            view = convertView;
            if (view == null)
            {
                view = _context.LayoutInflater.Inflate(Resource.Layout.MenuCustomLayout, parent, false);
                objMenuListViewHolderClass = new MenuListViewHolderClass();

                objMenuListViewHolderClass.txtMnuText = view.FindViewById<TextView>(Resource.Id.txtMnuText);
                objMenuListViewHolderClass.ivMenuImg = view.FindViewById<ImageView>(Resource.Id.ivMenuImg);

                objMenuListViewHolderClass.initialize(view);
                view.Tag = objMenuListViewHolderClass;
            }
            else
            {
                objMenuListViewHolderClass = (MenuListViewHolderClass)view.Tag;
            }
            objMenuListViewHolderClass.viewClicked = () =>
            {
                if (actionMenuSelected != null)
                {
                    actionMenuSelected(_mnuText[position]);
                }
            };
            objMenuListViewHolderClass.txtMnuText.Text = _mnuText[position];
            objMenuListViewHolderClass.ivMenuImg.SetImageResource(_mnuUrl[position]);
            return view;
        }
    }
    internal class MenuListViewHolderClass : Java.Lang.Object
    {
        internal Action viewClicked { get; set; }
        internal TextView txtMnuText;
        internal ImageView ivMenuImg;
        public void initialize(View view)
        {
            view.Click += delegate
            {
                viewClicked();
            };
        }

    }

    #endregion
}