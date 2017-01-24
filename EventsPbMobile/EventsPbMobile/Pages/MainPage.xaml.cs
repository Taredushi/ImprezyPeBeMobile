using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly EventsDataAccess _dataAccess;
        private bool isLoading;

        public bool IsLoading
        {
            get { return this.isLoading; }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MainPage()
        {
            InitializeComponent();
            App.Notification.ShowNotification("Chuj", "Dupa dupa dupa");
            App.Notification.StartService();
            IsLoading = false;
            BindingContext = this;
            _dataAccess = new EventsDataAccess();
            EventsList.ItemsSource = _dataAccess.Events;
            LastEventTitle.Text = "Tytuł ostatniego wydarzenia";
            CountDownTime();
        }

        private async void CountDownTime()
        {
            IsLoading = true;
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

        private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem == null) return;
            //cast object to event
            var _event = e.SelectedItem as EventViewModel;
            if (_event == null || _event.Event.Viewable == false) return;

            //deselect item just in case
            ((ListView)sender).SelectedItem = null;

            var eventdetails = new EventDetails(_event.Event) { BindingContext = _event.Event };
            await Navigation.PushAsync(eventdetails);
        }

        protected override async void OnAppearing()
        {
            IsLoading = true;
            var t = await _dataAccess.SaveEventsToDb();
            IsLoading = false;
        }
    }
}