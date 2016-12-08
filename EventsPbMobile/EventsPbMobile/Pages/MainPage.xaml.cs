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
            DateTime eventTime = DateTime.Now.AddMonths(1);
            TimeSpan remainingTime = eventTime - DateTime.Now;

            while (remainingTime.Seconds > 0)
            {
                remainingTime = eventTime - DateTime.Now;
                DaysLabel.Text = remainingTime.Days.ToString();
                HoursLabel.Text = remainingTime.Hours.ToString();
                MinutesLabel.Text = remainingTime.Minutes.ToString();
                SecondsLabel.Text = remainingTime.Seconds.ToString();

                await Task.Delay(250);
            }
        }

    }
}
