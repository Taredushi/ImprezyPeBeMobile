using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Realms;
using Xamarin.Forms;

namespace EventsPbMobile.Models
{
    public class Event : RealmObject
    {

        [PrimaryKey]
        public int EventId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Baner { get; set; }
        public bool Active { get; set; }
        public bool Viewable { get; set; }
        public IList<Activity> Activities { get; }
        public IList<PhotoEvent> PhotoEvents { get; }
        [Ignored]
        public string StringDate {
            get
            {
                var activity = Activities.OrderBy(x => x.StartHour).FirstOrDefault();
                return activity.StartHour.LocalDateTime.ToString("f");
            }
        }


        public Event() { }

        public Event(Event item)
        {
            EventId = item.EventId;
            StartDate = item.StartDate;
            EndDate = item.EndDate;
            Title = item.Title;
            Text = item.Text;
            Active = item.Active;
            Viewable = item.Viewable;
            Activities = item.Activities;
            PhotoEvents = item.PhotoEvents;
            Baner = item.Baner;
        }

        public Event(Event item, IList<Activity> activities)
        {
            EventId = item.EventId;
            StartDate = item.StartDate;
            EndDate = item.EndDate;
            Title = item.Title;
            Text = item.Text;
            Active = item.Active;
            Viewable = item.Viewable;
            Activities = activities;
            PhotoEvents = item.PhotoEvents;
            Baner = item.Baner;
        }
    }
}
