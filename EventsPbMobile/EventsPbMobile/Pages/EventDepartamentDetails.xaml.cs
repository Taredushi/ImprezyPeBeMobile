using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDepartamentDetails : ContentPage
    {
        private readonly Activity _activity;
        public EventDepartamentDetails(Activity act)
        {
            InitializeComponent();

            _activity = act;
            var db = new EventsDataAccess();
            Title = db.GetEventTitle(act.EventID);
            TitleLabel.Text = _activity.Title;
            PlaceLabel.Text = _activity.Place.Name;
            StartHourLabel.Text = _activity.StartHour.LocalDateTime.ToString("f");
			EndHourLabel.Text = _activity.EndHour.LocalDateTime.ToString("f");
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

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY >= (int)(this.ScrollView.ContentSize.Height - this.ScrollView.Height))
            {
                this.ScrollView.InputTransparent = true;
            }
            else
            {
                this.ScrollView.InputTransparent = false;
            }
        }
    }
}