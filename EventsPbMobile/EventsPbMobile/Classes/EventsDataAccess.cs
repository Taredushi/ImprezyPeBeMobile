using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Interface;
using EventsPbMobile.Models;
using SQLite;
using Xamarin.Forms;

namespace EventsPbMobile.Classes
{
    class EventsDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Event> Events { get; set; }

        public EventsDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Event>();
            this.Events =
              new ObservableCollection<Event>(database.Table<Event>());
        }

        //W celach testowych
        public void AddEvents()
        {
            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                Events.Add(new Event()
                {
                    Title = "Event " + i,
                    Active = random.Next(10)%2 == 0,
                    Text = RandomString(random.Next(20))
                });
            }
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<Event> GetActiveEvents()
        {
            lock (collisionLock)
            {
                var result = database.Table<Event>().Where(x=>x.Active).ToList();
                return result;
            }
        }

        public void SaveDataToDb()
        {
            lock (collisionLock)
            {
                foreach (var ev in Events)
                {
                    database.Insert(ev);
                }
            }
        }
    }
}
