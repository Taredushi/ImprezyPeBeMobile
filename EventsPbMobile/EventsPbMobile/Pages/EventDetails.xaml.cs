using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails
    {
        private readonly EventsDataAccess _dataAccess;
        private readonly Event _event;

        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            InitFavButton();
            _dataAccess = new EventsDataAccess();
            var activities = new ObservableCollection<Activity>();
            activities.Clear();
            foreach (var activity in e.Activities)
            {
                var ac = new Activity(activity);
                ac.Place = _dataAccess.GetPlace(ac.PlaceID);
                activities.Add(ac);
            }

            EventPlaces.ItemsSource = activities;
        }

        private async void Counter()
        {
            var rt = _event.Date.Subtract(DateTime.Now);
            string hours, minutes, seconds;
            while (rt.TotalSeconds > 0)
            {
                rt = _event.Date.Subtract(DateTime.Now);
                hours = rt.Hours.ToString();
                minutes = rt.Minutes.ToString();
                seconds = rt.Seconds.ToString();
                if (hours.Length == 1)
                    hours = "0" + rt.Hours;
                if (minutes.Length == 1)
                    minutes = "0" + rt.Minutes;
                if (seconds.Length == 1)
                    seconds = "0" + rt.Seconds;
                TitleLabel.Text = "Start za: " + rt.Days + " dni, " + hours + ":" + minutes + ":" + seconds;
                await Task.Delay(250);
            }
        }

        private async void MapButton_OnClicked(object sender, EventArgs e)
        {
            var activities = _event.Activities;

            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(EventMap))
                await Navigation.PushAsync(new EventMap(activities));
        }

        private void Notification_Clicked(object sender, EventArgs e)
        {
            if (Settings.IsToggled)
                App.Notification.ShowNotification("Nadchodzące wydarzenie", _event.Title);
        }

        private void InitFavButton()
        {
            ToolbarItems.Add(new ToolbarItem("Remind", "alert.png",
                () => { Navigation.PushAsync(new ReminderNotifySelect(_event)); }));
        }

        private async void EventInDepartamentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var activity = e.SelectedItem as Activity;
            var place = _dataAccess.GetPlace(activity.PlaceID);
            await Navigation.PushAsync(new EventDepartamentDetails(activity, place));
        }
    }
}