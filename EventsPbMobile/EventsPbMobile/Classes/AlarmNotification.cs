using Xamarin.Forms;

namespace EventsPbMobile.Classes
{
    public static class AlarmNotification
    {
        public static void SetAlarms()
        {
            var db = new EventsDataAccess();
            var timesettings = db.GetSettings();
            var eventsReminder = db.GetEventsWithSetReminder();
            foreach (var ev in eventsReminder)
            {
                if (timesettings.Notify1HBefore)
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już za godzinę na Politechnice!", ev.Title, ev.EventId * 10000,
                            ev.StartDate.UtcDateTime.AddHours(-1));
            
                else
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000);

                if (timesettings.Notify1DBefore)
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już jutro na Politechnice!", ev.Title, ev.EventId * 10000 + 1,
                            ev.StartDate.UtcDateTime.AddDays(-1));

                else
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000 + 1);

                if (timesettings.Notify2DBefore)
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już za 2 dni na Politechnice!", ev.Title, ev.EventId * 10000 + 2,
                            ev.StartDate.UtcDateTime.AddDays(-2));

                else
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000 + 2);
            }
        }

        public static void DisaBleAlarms()
        {
            var db = new EventsDataAccess();
            var eventsReminder = db.GetEventsWithSetReminder();

            foreach (var ev in eventsReminder)
                for (var i = 0; i < 3; i++)
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000 + i);
        }
    }
}