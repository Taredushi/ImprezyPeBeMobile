using System.Collections.Generic;
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
            Title = "Mapa wydarzenia";
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(53.118293, 23.149717), Distance.FromMeters(300)));

            if (activities == null) return;
            foreach (var activity in activities)
            {
                var pin = new Pin();
                pin.Position = new Position(activity.Place.Latitude, activity.Place.Longitude);
                pin.Label = activity.Place.Name;

                pin.Clicked += async (sender, args) =>
                {
                   await Navigation.PushAsync(new EventDepartamentDetails());
                };

                MyMap.Pins.Add(pin);
            }
        }
    }
}