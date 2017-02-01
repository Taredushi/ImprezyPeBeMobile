using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Realms;

namespace EventsPbMobile.Classes
{
    internal class EventsDataAccess
    {
        private readonly ApiConnection api = new ApiConnection();
        private RealmConfiguration config;
        private Realm db;
        public ObservableCollection<EventViewModel> Events { get; set; }
        public EventsDataAccess()
        {
            Configuration();
            Events = new ObservableCollection<EventViewModel>();
            PopulateEventsCollectionFromDb();
        }

        

        private void Configuration()
        {
            config = RealmConfiguration.DefaultConfiguration;
            config.SchemaVersion = 2;
            db = Realm.GetInstance();
        }

        public void PopulateEventsCollectionFromDb()
        {
            var list = db.All<Event>();
            list = list.Where(x => x.Date > DateTimeOffset.Now);
            Events.Clear();
            foreach (var data in list)
                Events.Add(EventToViewModel(data));
        }

        private EventViewModel EventToViewModel(Event data)
        {
            var evm = new EventViewModel();
            evm.TimeLeft = data.Date.Subtract(DateTime.Now);
            evm.Event = data;

            return evm;
        }

        public Event GetLastEvent()
        {
            var pbevents = db.All<Event>().ToList();
            var pbevent = pbevents.Where(x => x.Date < DateTimeOffset.Now).OrderByDescending(x => x.Date).First();
            Debug.WriteLine(pbevent.Title + " " + pbevent.Date);
            return pbevent;
        }

        public async Task<bool> SaveEventsToDb()
        {
           // Realm.DeleteRealm(config);
            var items = await api.GetEventsAllAsync();

            db.Write(() =>
            {
                if (items == null) return;
                foreach (var item in items)
                {
                    var ev = new Event(item);
                    db.Add(ev, true);
                }
            });

            PopulateEventsCollectionFromDb();
            return true;
        }
    }
}