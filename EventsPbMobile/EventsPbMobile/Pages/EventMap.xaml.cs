using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventMap : ContentPage
    {
        public EventMap()
        {
            InitializeComponent();
            Title = "Mapa wydarzenia";
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(53.118293, 23.149717), Distance.FromMeters(300)));
        }
    }
}