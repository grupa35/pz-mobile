using Android.App;
using Android.Widget;
using Plugin.Connectivity;
using System;

namespace PZ.Sklep.Services
{
    public static class UITools
    {
        public static ProgressDialog CreateAndShowLoadingDialog(Activity self)
        {
            ProgressDialog progressdialog = new ProgressDialog(self);
            progressdialog.SetCancelable(false);
            progressdialog.Indeterminate = true;
            progressdialog.SetMessage("Pobieranie danych...");
            progressdialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressdialog.Show();
            return progressdialog;
        }

        public static void EndLoadingDialog(ProgressDialog p)
        {
            p.Hide();
            p.Dismiss();
        }


        public static void checkInternetConnection(Activity activity)
        {

            var context = Application.Context;

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {

                if (isConnected())
                {
                    Console.WriteLine("Jest internecik !");
                    Toast.MakeText(context, "Siec dziala!", ToastLength.Short).Show();
                    activity.Recreate();
                }
                else
                {
                    Console.WriteLine("No kurwa lipa mordeczko z internecikiem");
                    Toast.MakeText(context, "Brak polaczenia", ToastLength.Short).Show();
                }
            };
        }

        public static bool isConnected()
        {
            if (!CrossConnectivity.IsSupported)
                return true;

            return CrossConnectivity.Current.IsConnected;
        }
    }
}