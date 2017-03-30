using System;
using System.Linq;
using Newtonsoft.Json;
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
                if (ev == null)
                    continue;
                DateTime time;
                var activity = ev.Activities.OrderBy(x => x.StartHour).FirstOrDefault();
                if (activity == null)
                {
                    time = ev.StartDate.LocalDateTime;
                }
                else
                {
                    time = activity.StartHour.LocalDateTime;
                }
                if (timesettings.Notify1HBefore &&
                    DateTimeOffset.Now < time.AddHours(-1))
                {
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już za godzinę na Politechnice!", ev.Title, ev.EventId * 10000,
                            time.AddHours(-1));
                }

                else
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000);

                if (timesettings.Notify1DBefore && DateTimeOffset.UtcNow < time.AddDays(-1))
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już jutro na Politechnice!", ev.Title, ev.EventId * 10000 + 1,
                            time.AddDays(-1));

                else
                    DependencyService.Get<INotification>().CancelAlarm(ev.EventId * 10000 + 1);

                if (timesettings.Notify2DBefore && DateTimeOffset.UtcNow < time.AddDays(-2))
                    DependencyService.Get<INotification>()
                        .SetAlarm("Już za 2 dni na Politechnice!", ev.Title, ev.EventId * 10000 + 2,
                            time.AddDays(-2));

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