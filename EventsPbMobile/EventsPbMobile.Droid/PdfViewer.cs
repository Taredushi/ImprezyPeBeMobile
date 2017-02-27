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
using Debug = System.Diagnostics.Debug;
[assembly: Dependency(typeof(EventsPbMobile.Droid.PdfViewer))]
namespace EventsPbMobile.Droid
{
    public class PdfViewer : IPdfViewer
    {
        public PdfViewer()
        {
            
        }
        public void View(string filePath)
        {
            var downloadpath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            downloadpath = Path.Combine(downloadpath, filePath);
           /* if (!File.Exists(filePath))
            {
                Debug.WriteLine("Nie istnieje");
                return;
            }*/
            var file = new Java.IO.File(downloadpath);
            var uri = Android.Net.Uri.FromFile(file);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");

            if (!File.Exists(downloadpath))
            {
                Toast.MakeText(Forms.Context, "Nie ma takiego pliku!", ToastLength.Short).Show();
                return;
            }

            try
            {
                Forms.Context.StartActivity(intent);
                Toast.MakeText(Forms.Context, downloadpath, ToastLength.Short).Show();

            }
            catch (Exception e)
            {
                Toast.MakeText(Forms.Context, "Nie znaleziono pliku PDF", ToastLength.Short).Show();
                 Debug.WriteLine(e.Message);
            }
        }
    }
}