using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsPbMobile.Pages
{
    public partial class Settings : ContentPage
    {
        public static bool IsToggled { get; private set; }

        public Settings()
        {
            InitializeComponent();
        }

        private void PushNotificationsProperty(object sender, ToggledEventArgs e)
        {
            IsToggled = e.Value;
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
