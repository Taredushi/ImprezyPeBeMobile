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
        public bool IsOn { get; set; }

        public Settings()
        {
            InitializeComponent();
        }

        private void PushNotifierChanged(object sender, PropertyChangedEventArgs e)
        {
            IsOn = !IsOn;
            Debug.WriteLine(IsOn);
        }
    }
}
