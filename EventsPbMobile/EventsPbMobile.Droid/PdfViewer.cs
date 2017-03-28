using System;
using System.IO;
using Android.Content;
using EventsPbMobile.Classes;
using EventsPbMobile.Droid;
using Xamarin.Forms;
using Environment = Android.OS.Environment;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

[assembly: Dependency(typeof(PdfViewer))]

namespace EventsPbMobile.Droid
{
    public class PdfViewer : IPdfViewer
    {
        public void View(string filePath)
        {
            var downloadpath =
                Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).AbsolutePath;
            downloadpath = Path.Combine(downloadpath, filePath);
            var file = new File(downloadpath);
            var uri = Uri.FromFile(file);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, "application/pdf");

            if (!System.IO.File.Exists(downloadpath))
                return;

            try
            {
                Forms.Context.StartActivity(intent);
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}