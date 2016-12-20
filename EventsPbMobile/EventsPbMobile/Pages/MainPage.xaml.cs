using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage
    {
        private readonly EventsDataAccess _dataAccess;

        public MainPage()
        {
            InitializeComponent();
            _dataAccess = new EventsDataAccess();
            AddTestData();
            EventsList.Header = "poprzednie wydarzenie";
            EventsList.ItemsSource = _dataAccess.Events;
            CountDownTime();
        }

        private async void CountDownTime()
        {
            var eventTime = DateTime.Now.AddMonths(1);
            var remainingTime = eventTime - DateTime.Now;

            while (remainingTime.Seconds > 0)
            {
                var events = new List<EventViewModel>();
                events.AddRange(EventsList.ItemsSource.OfType<EventViewModel>());

                foreach (var ev in events)
                {
                    ev.TimeLeft = ev.Event.Date.Subtract(DateTime.Now);
                }

                /* DaysLabel.Text = remainingTime.Days.ToString();
                 HoursLabel.Text = remainingTime.Hours.ToString();
                 MinutesLabel.Text = remainingTime.Minutes.ToString();
                 SecondsLabel.Text = remainingTime.Seconds.ToString();*/
                //countdown.Text = remainingTime.Seconds.ToString();
                await Task.Delay(1000);
            }
        }

        private void BottomTitle_OnClicked(object sender, EventArgs e)
        {
            var da = new EventsDataAccess();
            da.AddEvents();
            da.SaveDataToDb();
            var test = da.GetEvents();
        }

        private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem == null) return;
            //cast object to event
            var _event = e.SelectedItem as Event;
            Debug.WriteLine(sender.ToString());

            //deselect item just in case
            ((ListView) sender).SelectedItem = null;
            var eventdetails = new EventDetails(_event) {BindingContext = _event};
            await Navigation.PushAsync(eventdetails);
        }

        private void AddTestData()
        {
            var da = new EventsDataAccess();
            da.AddEvents();
            da.SaveDataToDb();

        }
    }
}