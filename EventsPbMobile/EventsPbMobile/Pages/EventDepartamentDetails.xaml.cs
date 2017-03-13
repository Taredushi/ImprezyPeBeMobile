using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDepartamentDetails : ContentPage
    {
        private Activity activity;
        private Place place;
        private EventsDataAccess db;
        public EventDepartamentDetails(string title, Activity act, Place pla)
        {
            InitializeComponent();

            db = new EventsDataAccess();
            activity = act;
            place = pla;
            
            Title = title;
            TitleLabel.Text = activity.Title;
            PlaceLabel.Text = place.Name;
    //        DateLabel.Text = activity.StartHour.Date.ToString("D");
            StartHourLabel.Text = activity.StartHour.ToString("D");
   //         EndHourLabel.Text = activity.EndHour.ToString("t");
            DescriptionLabel.Text = activity.Text;

            InitMap();
        }


        private void InitMap()
        {
            var pin = new Pin();
            var place = db.GetPlace(activity.PlaceID);
            pin.Position = new Position(place.Latitude, place.Longitude);
            pin.Label = place.Name;

            MyMap.Pins.Add(pin);

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(place.Latitude, place.Longitude), Distance.FromMeters(300)));
        }
    }
}