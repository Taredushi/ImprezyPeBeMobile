using System.Collections.Generic;
using SQLite;


namespace EventsPbMobile.Models
{
    class Event
    {
        [PrimaryKey, AutoIncrement]
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
        public bool Viewable { get; set; }
        public bool Gameable { get; set; }

        public IList<Activity> Activities { get; }
        public IList<Photo> Photos { get; }
    }
}
