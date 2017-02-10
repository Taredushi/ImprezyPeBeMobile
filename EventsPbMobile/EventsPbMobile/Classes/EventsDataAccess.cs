using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using EventsPbMobile.Pages;
using Realms;

namespace EventsPbMobile.Classes
{
    internal class EventsDataAccess
    {
        private readonly ApiConnection api = new ApiConnection();
        private RealmConfiguration config;
        private Realm db;

        public EventsDataAccess()
        {
            Configuration();
            Events = new ObservableCollection<EventViewModel>();
            PopulateEventsCollectionFromDb();
        }

        public ObservableCollection<EventViewModel> Events { get; }

        private void Configuration()
        {
            config = RealmConfiguration.DefaultConfiguration;
            config.SchemaVersion = 5;
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
            return pbevent;
        }

        public async Task<bool> SaveEventsToDb()
        {
            //Realm.DeleteRealm(config);
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

        public Place GetPlace(int id)
        {
            var place = db.All<Place>().First(x => x.PlaceId == id);
            return place;
        }

        public IQueryable<EventReminder> GetEventtReminders(int eventID)
        {
            return db.All<EventReminder>().Where(x => x.EventID == eventID);
        }

        public void SaveReminderStatusOfEvent(Event e, ObservableCollection<ReminderNotifySelect.ReminderCell> times)
        {
           // var list = db.All<EventReminder>().Where(x => x.EventID == e.EventId);
            foreach (var cell in times)
                db.Write(() =>
                {
                    Debug.WriteLine(cell.TextDisplayed + " " + cell.Selected);
                    if (cell.Selected)
                    {
                        var reminderobject = new EventReminder(e.EventId, e, cell.ReminderTime);
                        Debug.WriteLine("SAVING: " + e.Title);
                        db.Add(reminderobject);
                    }
                });
        }
    }
}