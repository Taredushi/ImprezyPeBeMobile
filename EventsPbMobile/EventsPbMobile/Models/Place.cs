using System.Collections.Generic;
using System.Linq;
using System.Text;
using Realms;

namespace EventsPbMobile.Models
{
    public class Place : RealmObject
    {

        public Place()
        {
            
        }

        public Place(Place place)
        {
            PlaceId = place.PlaceId;
            Latitude = place.Latitude;
            Longitude = place.Longitude;
            Name = place.Name;
            Activities = place.Activities;
        }

        [PrimaryKey]
        public int PlaceId { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }

        public IList<Activity> Activities { get; }
    }
}