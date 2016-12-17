using SQLite;

namespace EventsPbMobile.Models
{
    class Place
    {
        [PrimaryKey, AutoIncrement]
        public int PlaceID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Name { get; set; }

    }
}
