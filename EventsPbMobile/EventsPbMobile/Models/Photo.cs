using System.Collections.Generic;
using Realms;

namespace EventsPbMobile.Models
{
    public class Photo : RealmObject
    {
        [PrimaryKey]
        public int PhotoId { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

        public IList<PhotoEvent> PhotoEvents { get; }
    }
}
