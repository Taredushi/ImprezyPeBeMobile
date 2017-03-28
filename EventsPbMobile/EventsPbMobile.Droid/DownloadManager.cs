using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EventsPbMobile.Classes;
using Xamarin.Forms;
using File = Java.IO.File;

[assembly: Dependency(typeof(EventsPbMobile.Droid.DownloadManager))]
namespace EventsPbMobile.Droid
{
    public class DownloadManager :IDownloadManager
    {
        private long _fileId;
        private Android.App.DownloadManager dm;
        public DownloadManager()
        {
            
        }
        public void Download(string uri, string filename)
        {
            var contentUri = Android.Net.Uri.Parse(uri);
            var r = new Android.App.DownloadManager.Request(contentUri);
            var downloadpath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

            var file = new File(Path.Combine(downloadpath, filename));
            if (file.Exists())
            {
                return;
            }
            r.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, filename);
            r.AllowScanningByMediaScanner();
            r.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
            dm = (Android.App.DownloadManager)Forms.Context.GetSystemService(Context.DownloadService);
            _fileId = dm.Enqueue(r);
            ProgressDialog dialog = new ProgressDialog(Forms.Context);
            dialog.Create();
            dialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            dialog.SetMessage("Pobieranie pliku");
            dialog.Show();
            dialog.Cancel();

        }

        public void OpenDownloadedFile()
        {
            
        }
    }
}