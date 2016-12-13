using System;
using System.Collections.Generic;
using System.Text;
using EventsPbMobile.iOS.Classes;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace EventsPbMobile.iOS.Classes
{
    class DatabaseConnection_iOS
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "PbDb.db3";
            string personalFolder =
              System.Environment.
              GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}
