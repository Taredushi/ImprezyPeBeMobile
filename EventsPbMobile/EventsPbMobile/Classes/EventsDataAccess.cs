using System;
using System.Collections.Generic;
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
            config.SchemaVersion = 14;
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
           // Realm.DeleteRealm(config);
            var items = await api.GetEventsAllAsync();
            var events = db.All<Event>();
            if (!events.Any())
            {
                db.Write(() =>
                {
                    if (items == null) return;
                    foreach (var item in items)
                    {
                        var ev = new Event(item);
                        db.Add(ev, true);
                    }
                });
            }
            

            PopulateEventsCollectionFromDb();
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
                db.Add(eventreminder,true);
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
            {
                foreach (var ev in events)
                {
                    if (!eventreminders.Any(x => x.EventId ==ev.EventId))
                    {
                        db.Write(() =>
                        {
                            var evrem = new EventReminder(ev.EventId,false);
                            db.Add(evrem);
                        });
                    }
                }
            }
            return db.All<EventReminder>().FirstOrDefault(x => x.EventId == eventid);
        } 
    }
}