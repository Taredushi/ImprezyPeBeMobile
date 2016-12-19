using SQLite;

namespace EventsPbMobile.Models
{
    public class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int PhotoId { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

    }
}
