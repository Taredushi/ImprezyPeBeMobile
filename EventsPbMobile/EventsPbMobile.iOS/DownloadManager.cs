using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UIKit;
using Xam.Plugin.DownloadManager.Abstractions;

namespace EventsPbMobile.iOS
{
    public class DownloadManager :IDownloadManager
    {
        public void Download(string uri, string filename)
        {
            var webClient = new WebClient();

            webClient.DownloadDataCompleted += (s, e) =>
            {
                var bytes = e.Result; // get the downloaded data
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var localFilename = filename;
                var localPath = Path.Combine(documentsPath, localFilename);
                File.WriteAllBytes(localPath, bytes); // writes to local storage   
            };

            var url = new Uri(uri);

            webClient.DownloadDataAsync(url);

          //  new UIAlertView("Done", "Download Done.", null, "OK", null).Show();
        }
    }
}
