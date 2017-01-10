using Realms;

namespace EventsPbMobile.Models
{
    public class PhotoEvent : RealmObject
    {
        [PrimaryKey]
        public int PhotoEventID { get; set; }

        public int PhotoID { get; set; }

        public int EventID { get; set; }

        public Photo Photo { get; set; }
    }
}
