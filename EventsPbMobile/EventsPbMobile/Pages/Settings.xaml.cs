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
        private bool IsToggled { get; set; }

        public Settings()
        {
            InitializeComponent();
        }

        private void PushNotificationsProperty(object sender, ToggledEventArgs e)
        {
            IsToggled = e.Value;
        }
    }
}
