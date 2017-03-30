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
            config = new RealmConfiguration();
            config.SchemaVersion = 20;
            db = Realm.GetInstance(config);
        }

        public Realm GetDbInstance()
        {
            return db;
        }

        private void PopulateEventsCollectionFromDb()
        {
            var list = db.All<Event>().Where(x => x.Viewable).OrderBy(x => x.StartDate);
            var events = new List<Event>();
            foreach (var ev in list)
            {
                if (ev.EndDate.LocalDateTime >= DateTime.Today)
                {
                    events.Add(ev);
                }
            }
            Events.Clear();
            foreach (var data in events)
                Events.Add(EventToViewModel(data));
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

            var activitiesgood = await SaveActivitiesToDb();

            db.Write(() =>
            {
                if (items == null) return;

                var eventsSetToDelete = db.All<Event>();
                var eventsReminderDelete = db.All<EventReminder>();
                foreach (var eventdelete in eventsSetToDelete)
                {
                    if (items.All(x => x.EventId != eventdelete.EventId))
                        db.Remove(eventdelete);

                }
                foreach (var reminder in eventsReminderDelete)
                {
                    var id = reminder.EventId;
                    if (items.All(x=>x.EventId != id))
                    {
                        db.Remove(reminder);
                    }
                }
                    

                foreach (var item in items)
                {
                    var activities = db.All<Activity>().Where(x => x.EventID == item.EventId).ToList();
                    var ev = new Event(item, activities);
                    db.Add(ev, true);
                }
            });
            PopulateEventsCollectionFromDb();
            return true;
        }


        private async Task<bool> SavePlacesToDb()
        {
            var items = await api.GetPlacesAllAsync();
            


            db.Write(() =>
            {
                if (items == null) return;

                var placesSetToDelete = db.All<Place>();

                foreach (var place in placesSetToDelete) 
                    if (items.All(x => x.PlaceId != place.PlaceId))
                        db.Remove(place);

                string latitude, longitude;
                foreach (var place in items)
                {
                    if (place.Latitude.ToString().Contains("+"))
                    {
                        latitude = place.Latitude.ToString();
                        latitude = latitude.Replace("E+07", "");
                        place.Latitude = float.Parse(latitude)*10;
                    }
                    if (place.Longitude.ToString().Contains("+"))
                    {
                        longitude = place.Longitude.ToString("");
                        longitude = longitude.Replace("E+07", "");
                        place.Longitude = float.Parse(longitude)*10;
                    }
                    
                    var pl = new Place(place);
                    db.Add(pl, true);
                }
            });
            return true;
        }

        private async Task<bool> SaveActivitiesToDb()
        {
            var items = await api.GetActivitiesAllAsync();

            var places = await SavePlacesToDb();

            db.Write(() =>
            {
                if (items == null) return;

                var activitiesSetToDelete = db.All<Activity>();

                foreach (var activity in activitiesSetToDelete)
                    if (items.All(x => x.ActivityID != activity.ActivityID))
                        db.Remove(activity);

                foreach (var activity in items)
                {
                    var place = db.All<Place>().FirstOrDefault(x => x.PlaceId == activity.PlaceID);
                    var act = new Activity(activity,place);
                    db.Add(act, true);
                }
            });

            return true;
        }

        public void SaveEventWithSetReminder(int eventId)
        {
            db.Write(() =>
            {
                var eventreminder = db.Find("EventReminder", eventId) as EventReminder;
                eventreminder.NotificationEnabled = true;
                db.Add(eventreminder, true);
            });
        }

        public void RemoveEventWithSetReminder(int eventId)
        {
            db.Write(() =>
            {
                var eventreminder = db.Find("EventReminder", eventId) as EventReminder;
                if (eventreminder == null) return;
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
                var id = eventReminder.EventId;
                var events = db.All<Event>().FirstOrDefault(x => x.EventId == id);
                listofevents.Add(events);
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

        public string GetEventTitle(int eventId)
        {
            var ev =  db.All<Event>().FirstOrDefault(x => x.EventId == eventId);
            return ev.Title;
        } 

        public Settings GetSettings()
        {
            var settings = db.All<Settings>().FirstOrDefault(x => x.SettingsId == 1);
            if (settings == null)
                db.Write(() =>
                {
                    var stng = new Settings
                    {
                        SettingsId = 1,
                        NotificationsEnabled = true,
                        Notify1DBefore = true,
                        Notify1HBefore = false,
                        Notify2DBefore = false,
                        LastRefreshDate = DateTimeOffset.Now.AddDays(-2),
                        AhotherLauchOfApp = false
                    };
                    db.Add(stng, true);
                });
            settings = db.All<Settings>().FirstOrDefault(x => x.SettingsId == 1);

            return settings;
        }

        public void SaveSettings(Settings s)
        {
            db.Write(() => { db.Add(s, true); });
        }

        public IList<Activity> GetActivities()
        {
            return db.All<Activity>().ToList();
        }
    }
}