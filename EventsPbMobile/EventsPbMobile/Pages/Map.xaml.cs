using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class Map : ContentPage
    {
        public Map()
        {
            InitializeComponent();
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(53.118293, 23.149717), Distance.FromMeters(300)));

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
