using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Realms;

namespace EventsPbMobile.Classes
{
    class EventsDataAccess
    {
        private ApiConnection api = new ApiConnection();
        private Realm db;
        private RealmConfiguration config;

        public ObservableCollection<EventViewModel> Events { get; set; }

        public EventsDataAccess()
        {
            Configuration();
            this.Events = new ObservableCollection<EventViewModel>();
            SaveEventsToDb();
        }

        private void Configuration()
        {
            config = RealmConfiguration.DefaultConfiguration;
            config.SchemaVersion = 1;
            db = Realm.GetInstance();

        }

        public  void PopulateEventsCollectionFromDb()
        {
            var list = db.All<Event>();
            foreach (var data in list)
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

        public async void SaveEventsToDb()
        {
            var items = await api.GetEventsAllAsync();

            db.Write(() =>
            {
                foreach (var item in items)
                {
                    var ev = new Event(item);
                    db.Add(ev, true);
                }
            });

            PopulateEventsCollectionFromDb();

        }
    }
}
