using Realms;

namespace EventsPbMobile.Models
{
    public class EventReminder : RealmObject
    {
        public EventReminder()
        {
        }

        public EventReminder(int eventid, bool notstatus)
        {
            EventId = eventid;
            NotificationEnabled = notstatus;
        }
        [PrimaryKey]
        public int EventId { get; set; }
        public bool NotificationEnabled { get; set; }
       
    }
}