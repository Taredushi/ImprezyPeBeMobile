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
            Title = a.Title;
            Text = a.Text;
            StartHour = a.StartHour;
            EndHour = a.EndHour;
            Gameable = a.Gameable;
            PlaceID = a.PlaceID;
            EventID = a.EventID;
            Place = a.Place;
        }

        [PrimaryKey]
        public int ActivityId { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset StartHour { get; set; }
        public DateTimeOffset EndHour { get; set; }
        public bool Gameable { get; set; }
        public int PlaceID { get; set; }
        public int EventID { get; set; }

        public Place Place { get; set; }
    }
}