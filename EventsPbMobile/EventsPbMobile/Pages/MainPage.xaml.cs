using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            CountDownTime();

        }

        private async void CountDownTime()
        {

            while (true)
            {
                TimerLabel.Text = DateTime.Now.ToString("T");
                await Task.Delay(250);
            }
        }

    }
}
