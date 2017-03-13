using System;
using Realms;

namespace EventsPbMobile.Models
{
    public class Activity : RealmObject
    {
        public Activity()
        {
        }

        public Activity(Activity a)
        {
            ActivityID = a.ActivityID;
            Title = a.Title;
            Text = a.Text;
            StartHour = a.StartHour;
            EndHour = a.EndHour;
            PlaceID = a.PlaceID;
            EventID = a.EventID;
            Place = a.Place;
            Date = a.Date;
            Event = a.Event;
        }

        [PrimaryKey]
        public int ActivityID { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset StartHour { get; set; }
        public DateTimeOffset EndHour { get; set; }
        public int PlaceID { get; set; }
        public Place Place { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        [Ignored]
        public string PlaceAndDate => Place.Name + ", " + StartHour.Date.ToString("f");
    }
}