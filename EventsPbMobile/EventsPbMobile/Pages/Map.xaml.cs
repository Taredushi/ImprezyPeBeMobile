using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace EventsPbMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : TabbedPage
    {
        public Map()
        {
            InitializeComponent();
            //this!
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.DisableSwipePaging(On<Android>());
            InitBilaystokMap();
            InitHajnowkaMap();
        }

        private void InitBilaystokMap()
        {
            var bialystokPins = new List<Pin>
            {
                new Pin
                {
                    Address = "Wiejska 45D, 15-352 Białystok",
                    Label = "Kampus PB",
                    Position = new Position(53.1178927, 23.1482911)
                },
                new Pin
                {
                    Address = "Oskara Sosnowskiego 11, 15-351 Białystok",
                    Label = "Wydział Architektury",
                    Position = new Position(53.1216265, 23.1416531)
                },
                new Pin
                {
                    Address = "Ojca Tarasiuka 2, 16-001 Kleosin",
                    Label = "Wydział Zarządzania",
                    Position = new Position(53.0983544, 23.1232682)
                }
            };


            BialystokGoogleMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(53.1104326, 23.1324947), Distance.FromMeters(1400)));

            foreach (var pin in bialystokPins)
                BialystokGoogleMap.Pins.Add(pin);
        }

        private void InitHajnowkaMap()
        {
            var hajnowkaPin = new Pin
            {
                Address = "marsz. Józefa Piłsudskiego 8, 17-200 Hajnówka",
                Label = "Zamiejscowy Wydział Zarządzania Środowiskiem",
                Position = new Position(52.736124, 23.5893153)
            };

            HajnowkaGoogleMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(52.736124, 23.5893153),
                Distance.FromMeters(400)));

            HajnowkaGoogleMap.Pins.Add(hajnowkaPin);
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
            return true;
        }
    }
}