using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        public ObservableCollection<EventViewModel> Events { get; set; }

        public EventsDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Event>();
            this.Events =
              new ObservableCollection<EventViewModel>();
            PopulateEventsCollectionFromDb();
        }

        private void PopulateEventsCollectionFromDb()
        {
            foreach (var data in database.Table<Event>().ToList())
            {
                Events.Add(EventToViewModel(data));
            }
        }
        private EventViewModel EventToViewModel(Event data)
        {
            var evm = new EventViewModel();
            evm.TimeLeft = data.Date.Subtract(DateTime.Now);
            evm.Event = data;

            return evm;
        }

        //W celach testowych
        public void AddEvents()
        {
            Random random = new Random();
            for (int i = 1; i <= 5; i++)
            {
                Events.Add(new EventViewModel()
                {
                    Event = new Event()
                    {
                        Title = "Event " + i,
                        Viewable = random.NextDouble() >= 0.5,
                        Date = DateTime.Now.AddDays(random.Next(3, 20)),
                        Text = RandomString(random.Next(20)),
                        Active = random.NextDouble() >= 0.5
                    }
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

        public IEnumerable<Event> GetEvents()
        {
            lock (collisionLock)
            {
                var result = database.Table<Event>().ToList();
                return result;
            }
        }

        public void SaveDataToDb()
        {
            lock (collisionLock)
            {
                foreach (var ev in Events)
                {
                    database.Insert(ev.Event);
                }
            }
        }

        public void RemoveEvents()
        {
            lock (collisionLock)
            {
                database.DeleteAll<Event>();
            }
        }
    }
}
