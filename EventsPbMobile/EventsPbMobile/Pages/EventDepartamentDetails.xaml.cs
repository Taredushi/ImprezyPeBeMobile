using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class EventDepartamentDetails : ContentPage
    {
        public EventDepartamentDetails(Activity activity, Place place)
        {
            InitializeComponent();

            PlaceLat.Text = "lat:" + place.Latitude;
            PlaceLong.Text = "long:" + place.Longitude;
        }
    }
}