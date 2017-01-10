using System;
using System.Collections.Generic;
using Realms;

namespace EventsPbMobile.Models
{
    public class Event : RealmObject
    {

        [PrimaryKey]
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool Active { get; set; }
        public bool Viewable { get; set; }
        public bool Gameable { get; set; }
        public IList<Activity> Activities { get; }
        public IList<Photo> PhotoEvents { get; }
        public IList<UserEvent> UserGames { get; }

        public Event() { }

        public Event(Event item)
        {
            EventId = item.EventId;
            Title = item.Title;
            Text = item.Text;
            Date = item.Date;
            Active = item.Active;
            Viewable = item.Viewable;
            Gameable = item.Gameable;
            Activities = item.Activities;
            PhotoEvents = item.PhotoEvents;
            UserGames = item.UserGames;
        }
    }
}
