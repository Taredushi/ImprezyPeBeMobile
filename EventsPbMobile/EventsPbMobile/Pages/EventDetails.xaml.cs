using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails : ContentPage
    {
        private readonly Event _event;
        public EventDetails(Event e)
        {
            this._event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            
        }

        private async void Counter()
        {
            var rt = _event.Date.Subtract(DateTime.Now);
            while (rt.TotalSeconds > 0)
            {
                rt = _event.Date.Subtract(DateTime.Now);
                DaysLabel.Text = rt.Days.ToString();
                HoursLabel.Text = rt.Hours.ToString();
                MinutesLabel.Text = rt.Minutes.ToString();
                SecondsLabel.Text = rt.Seconds.ToString();
                await Task.Delay(250);
            }
        }

        private void MapButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EventMap());
        }
    }
}
