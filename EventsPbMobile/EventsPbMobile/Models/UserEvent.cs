using Realms;

namespace EventsPbMobile.Models
{
    public class UserEvent : RealmObject
    {
        [PrimaryKey]
        public int UserEventID { get; set; }
        public string UserID { get; set; }
        public int EventID { get; set; }
        public int Points { get; set; }

        public Event Event { get; set; }
        //public User User { get; set; }
    }
}
