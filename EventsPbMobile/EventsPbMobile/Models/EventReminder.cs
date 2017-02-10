using Realms;

namespace EventsPbMobile.Models
{
    public class EventReminder : RealmObject
    {
        public EventReminder()
        {
        }


        public EventReminder(int eventid, Event e, int notificationtime)
        {
            Event = e;
            EventID = eventid;
            NotificationTime = notificationtime;
        }

        public int EventID { get; set; }
        public Event Event { get; set; }

        public int NotificationTime { get; set; }
    }
}