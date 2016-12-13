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
using EventsPbMobile.Interface;
using SQLite;
using System.IO;
using EventsPbMobile.Droid.Classes;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace EventsPbMobile.Droid.Classes
{
    class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "PbDb.db3";
            var path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
        }
    }
}