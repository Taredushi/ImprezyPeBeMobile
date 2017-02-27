using Realms;

namespace EventsPbMobile.Models
{
    public class PhotoEvent : RealmObject
    {
        public PhotoEvent()
        {
        }

        public PhotoEvent(PhotoEvent photoEvent)
        {
            PhotoEventID = photoEvent.PhotoEventID;
            PhotoID = photoEvent.PhotoID;
            EventID = photoEvent.EventID;
            Photo = photoEvent.Photo;
        }

        [PrimaryKey]
        public int PhotoEventID { get; set; }

        public int PhotoID { get; set; }

        public int EventID { get; set; }

        public Photo Photo { get; set; }
    }
}