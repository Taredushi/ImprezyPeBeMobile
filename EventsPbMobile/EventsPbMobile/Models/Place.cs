using SQLite;

namespace EventsPbMobile.Models
{
    public class Place
    {
        [PrimaryKey, AutoIncrement]
        public int PlaceId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }

    }
}
