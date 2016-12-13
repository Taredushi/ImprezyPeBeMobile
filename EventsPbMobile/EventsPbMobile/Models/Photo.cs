using SQLite;

namespace EventsPbMobile.Models
{
    class Photo
    {
        [PrimaryKey, AutoIncrement]
        public int PhotoID { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }

    }
}
