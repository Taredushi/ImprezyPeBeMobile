using Realms;

namespace EventsPbMobile.Models
{
    public class EventReminder : RealmObject
    {
        public EventReminder()
        {
        }


        public EventReminder(int eventId, int notificationtime)
        {
            EventID = eventId;
            NotificationTime = notificationtime;
        }
        public int EventID { get; set; }

        public int NotificationTime { get; set; }
    }
}