using System.Collections.Generic;
using Realms;

namespace EventsPbMobile.Models
{
    public class Place : RealmObject
    {
        [PrimaryKey]
        public int PlaceId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }
        public IList<Activity> Activities { get; }

    }
}
