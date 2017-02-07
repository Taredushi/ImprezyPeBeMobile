using System.Collections.Generic;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventMap : ContentPage
    {
        public EventMap(IList<Activity> activities)
        {
            InitializeComponent();
            var dataAccess = new EventsDataAccess();
            Title = "Mapa wydarzenia";
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(53.118293, 23.149717), Distance.FromMeters(300)));

            if (activities == null) return;
            foreach (var activity in activities)
            {
                var pin = new Pin();
                var place = dataAccess.GetPlace(activity.PlaceID);
                pin.Position = new Position(place.Latitude, place.Longitude);
                pin.Label = place.Name;

                pin.Clicked += async (sender, args) =>
                {
                   await Navigation.PushAsync(new EventDepartamentDetails(activity, place));
                };

                MyMap.Pins.Add(pin);
            }
        }
    }
}