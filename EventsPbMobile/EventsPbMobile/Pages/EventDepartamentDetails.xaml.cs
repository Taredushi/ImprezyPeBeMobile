using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDepartamentDetails : ContentPage
    {
        private readonly Activity _activity;
        public EventDepartamentDetails(string title, Activity act)
        {
            InitializeComponent();

            _activity = act;
           
            Title = title;
            TitleLabel.Text = _activity.Title;
            PlaceLabel.Text = _activity.Place.Name;
            StartHourLabel.Text = _activity.StartHour.ToString("f");
			EndHourLabel.Text = _activity.EndHour.ToString("f");
            DescriptionLabel.Text = _activity.Text;

            InitMap();
        }


        private void InitMap()
        {
            var pin = new Pin();
            var place = _activity.Place;
            pin.Position = new Position(place.Latitude, place.Longitude);
            pin.Label = place.Name;

            MyMap.Pins.Add(pin);

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(place.Latitude, place.Longitude), Distance.FromMeters(300)));
        }
    }
}