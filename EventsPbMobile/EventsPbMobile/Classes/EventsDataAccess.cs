using System;
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
            config.SchemaVersion = 17;
            db = Realm.GetInstance();
        }

        private void PopulateEventsCollectionFromDb()
        {
            var list = db.All<Event>();
            //NIE ZAPOMNIEC PRZYWROCIC!
          //  list = list.Where(x => x.Date > DateTimeOffset.Now);
            Events.Clear();
            foreach (var data in list)
                Events.Add(EventToViewModel(data));

        }

        public IList<Activity> GetActivitiesForEvent(int eventId)
        {

            var activities = db.All<Activity>().Where(x => x.EventID == eventId).ToList();
            return activities;
        }

        private EventViewModel EventToViewModel(Event data)
        {
            var evm = new EventViewModel();
            evm.TimeLeft = data.StartDate.Subtract(DateTime.Now);
            evm.Event = data;

            return evm;
        }


        public async Task<bool> SaveEventsToDb()
        {
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

        public async Task<bool> SavePhotosToDb()
        {
            var items = await api.GetPhotosAllAsync();
            db.Write(() =>
            {
                if (items == null) return;
                foreach (var item in items)
                {
                    var photo = new Photo(item);
                    db.Add(photo, true);
                }
            });
            return true;
        }

        public async Task<bool> SavePlacesToDb()
        {
            var items = await api.GetPlacesAllAsync();

            db.Write(() =>
            {
                if (items == null) return;
                foreach (var place in items)
                {
                    var pl = new Place(place);
                    db.Add(pl, true);
                }
            });
            return true;
        }

        public async Task<bool> SaveActivitiesToDb()
        {
            var items = await api.GetActivitiesAllAsync();
            
            db.Write(() =>
            {
                if (items == null) return;
                foreach (var activity in items)
                {
                    var act = new Activity(activity);
                    db.Add(act, true);
                }
            });

            return true;
        }

        public async Task<bool> SavePhotoEventsToDb()
        {
            var items = await api.GetPhotoEventsAllAsync();

            db.Write(() =>
            {
                if (items == null) return;
                foreach (var photoEvent in items)
                {
                    var pe = new PhotoEvent(photoEvent);
                    db.Add(pe,true);
                }
            });
            return true;
        }

        public Place GetPlace(int id)
        {
            var place = db.All<Place>().First(x => x.PlaceId == id);
            return place;
        }

        public void SaveReminderSettings(EventReminder reminder)
        {
            db.Write(() =>
            {
                db.RemoveAll<EventReminder>();
                db.Add(reminder);
            });
        }

        public void SaveEventWithSetReminder(int eventId)
        {
            db.Write(() =>
            {
                var eventreminder = db.Find("EventReminder", eventId) as EventReminder;
                eventreminder.NotificationEnabled = true;
                db.Add(eventreminder, true);
                var xx = db.All<EventReminder>();
                Debug.WriteLine(xx.Count() + " count");
            });
        }

        public void RemoveEventWithSetReminder(int eventId)
        {
            db.Write(() =>
            {
                var eventreminder = db.Find("EventReminder", eventId) as EventReminder;
                eventreminder.NotificationEnabled = false;
                db.Add(eventreminder, true);
            });
        }

        public IEnumerable<Event> GetEventsWithSetReminder()
        {
            var listeventreminder = db.All<EventReminder>().Where(x => x.NotificationEnabled);
            var listofevents = new List<Event>();
            foreach (var eventReminder in listeventreminder)
            {
                var ev = db.Find("Event", eventReminder.EventId) as Event;
                listofevents.Add(ev);
            }
            return listofevents;
        }

        public EventReminder GetEventReminder(int eventid)
        {
            var eventreminders = db.All<EventReminder>();
            var events = db.All<Event>();
            if (eventreminders.Count() != events.Count())
                foreach (var ev in events)
                    if (!eventreminders.Any(x => x.EventId == ev.EventId))
                        db.Write(() =>
                        {
                            var evrem = new EventReminder(ev.EventId, false);
                            db.Add(evrem);
                        });
            return db.All<EventReminder>().FirstOrDefault(x => x.EventId == eventid);
        }

        public void DeleteDatabase()
        {
            Realm.DeleteRealm(config);
        }
    }
}