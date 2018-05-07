using Android.App;

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
    }
}