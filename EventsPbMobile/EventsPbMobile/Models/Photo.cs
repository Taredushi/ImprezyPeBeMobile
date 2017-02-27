using System.Collections.Generic;
using Realms;

namespace EventsPbMobile.Models
{
    public class Photo : RealmObject
    {
        public Photo()
        {
            
        }

        public Photo(Photo photo)
        {
            PhotoId = photo.PhotoId;
            Source = photo.Source;
            Text = photo.Text;
            PhotoEvents = photo.PhotoEvents;
        }
        [PrimaryKey]
        public int PhotoId { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

        public IList<PhotoEvent> PhotoEvents { get; }
    }
}
