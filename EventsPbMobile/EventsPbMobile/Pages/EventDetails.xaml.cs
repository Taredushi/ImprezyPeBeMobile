using System;
using System.Collections.Generic;
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
        private IList<Activity> activities;
        private ObservableCollection<Activity> evenActivities;
        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();

            _dataAccess = new EventsDataAccess();
            evenActivities = new ObservableCollection<Activity>();
            activities = _dataAccess.GetActivitiesForEvent(e.EventId);
            evenActivities.Clear();

            foreach (var activity in activities)
            {
                var ac = new Activity(activity);
                ac.Place = _dataAccess.GetPlace(ac.PlaceID);
                evenActivities.Add(ac);
            }
            InitFavButton();
            EventPlaces.ItemsSource = evenActivities;
        }

        private async void Counter()
        {
            var rt = _event.StartDate.Subtract(DateTime.Now);
            string hours, minutes, seconds;
            while (rt.TotalSeconds > 0)
            {
                rt = _event.StartDate.Subtract(DateTime.Now);
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
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(EventMap))
                await Navigation.PushAsync(new EventMap(evenActivities));
        }

        private void Notification_Clicked(object sender, EventArgs e)
        {
            if (Settings.IsToggled)
                App.Notification.ShowNotification("Nadchodzące wydarzenie", _event.Title);
        }

        private void InitFavButton()
        {
            var eventreminder = _dataAccess.GetEventReminder(_event.EventId);
            bool answer;
            if (eventreminder.NotificationEnabled)
                ToolbarItems.Add(new ToolbarItem("Remind", "notidisabled.png", async () =>
                {
                    answer = await DisplayAlert("Powiadomienie",
                        "Czy chcesz wyłączyć powiadomienie dla tego wydarzenie?", "Tak", "Nie");
                    Debug.WriteLine(answer);
                    if (answer) 
                    {
                        _dataAccess.RemoveEventWithSetReminder(_event.EventId);
                    }
                }));
            else
                ToolbarItems.Add(new ToolbarItem("Remind", "notiactive.png", async () =>
                {
                    answer = await DisplayAlert("Powiadomienie", "Czy chcesz ustawić powiadomienie na to wydarzenie?",
                        "Tak", "Nie");
                    if (answer)
                    {
                        _dataAccess.SaveEventWithSetReminder(_event.EventId);
                    }
                }));
        }

        private async void EventInDepartamentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var activity = e.SelectedItem as Activity;
            var place = _dataAccess.GetPlace(activity.PlaceID);
            await Navigation.PushAsync(new EventDepartamentDetails(activity, place));
        }
    }
}