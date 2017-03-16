using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails
    {
        private readonly EventsDataAccess _dataAccess;
        private readonly Event _event;
        private readonly ObservableCollection<Activity> _eventActivities;
        private ToolbarItem _enableNotificationItem, _disableNotificationItem;

        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            _dataAccess = new EventsDataAccess();
            _eventActivities = new ObservableCollection<Activity>();
            var activities = _dataAccess.GetActivitiesForEvent(e.EventId);
            _eventActivities.Clear();

            foreach (var activity in activities)
            {
                var ac = new Activity(activity);
                ac.Place = _dataAccess.GetPlace(ac.PlaceID);
                _eventActivities.Add(ac);
            }
            InitToolbarItems();
            InitFavButton();
            InitEventMap();

            EventPlaces.ItemsSource = _eventActivities;
        }

        private async void Counter()
        {
            var start = _event.StartDate.Subtract(DateTimeOffset.Now);
            var end = _event.EndDate.Subtract(DateTimeOffset.Now);

            string hours, minutes, seconds;
            if (start.TotalSeconds > 0)
                while (start.TotalSeconds > 0)
                {
                    start = _event.StartDate.Subtract(DateTime.Now);
                    hours = start.Hours.ToString();
                    minutes = start.Minutes.ToString();
                    seconds = start.Seconds.ToString();
                    if (hours.Length == 1)
                        hours = "0" + start.Hours;
                    if (minutes.Length == 1)
                        minutes = "0" + start.Minutes;
                    if (seconds.Length == 1)
                        seconds = "0" + start.Seconds;
                    TitleLabel.Text = "Start za: " + start.Days + " dni, " + hours + ":" + minutes + ":" + seconds;
                    await Task.Delay(250);
                }

            else if (start.TotalSeconds <= 0 && end.TotalSeconds > 0)
                while (end.TotalSeconds > 0)
                {
                    TitleLabel.Text = "Event trwa!";
                    await Task.Delay(10000);
                }
            else
                TitleLabel.Text = "Event zakończony";
        }

        private async void EventInDepartamentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null || ((ListView) sender).SelectedItem == null) return;

            var activity = e.SelectedItem as Activity;
            var place = _dataAccess.GetPlace(activity.PlaceID);

            await Navigation.PushAsync(new EventDepartamentDetails(Title, activity, place));

            ((ListView) sender).SelectedItem = null;
        }

        private void InitToolbarItems()
        {
            _enableNotificationItem = new ToolbarItem("Remind", "ic_notifications_active_white_36dp.png", async () =>
            {
                var answer = await DisplayAlert("Powiadomienie", "Czy chcesz ustawić powiadomienie na to wydarzenie?",
                    "Tak", "Nie");

                if (!answer) return;

                _dataAccess.SaveEventWithSetReminder(_event.EventId);
                InitFavButton();
            });

            _disableNotificationItem = new ToolbarItem("Remind", "ic_notifications_off_white_36dp.png", async () =>
            {
                var answer = await DisplayAlert("Powiadomienie",
                    "Czy chcesz wyłączyć powiadomienie dla tego wydarzenie?", "Tak", "Nie");

                if (!answer) return;

                _dataAccess.RemoveEventWithSetReminder(_event.EventId);
                InitFavButton();
            });
        }

        private void InitFavButton()
        {
            var eventreminder = _dataAccess.GetEventReminder(_event.EventId);
            if (eventreminder.NotificationEnabled)
            {
                if (ToolbarItems.Count > 0)
                    ToolbarItems.RemoveAt(0);
                ToolbarItems.Add(_disableNotificationItem);
            }
            else
            {
                if (ToolbarItems.Count > 0)
                    ToolbarItems.RemoveAt(0);
                ToolbarItems.Add(_enableNotificationItem);
            }
        }

        private void InitEventMap()
        {
            float avgLatitude = 0, avgLongitude = 0;
            foreach (var activity in _eventActivities)
            {
                var pin = new Pin();
                var place = _dataAccess.GetPlace(activity.PlaceID);
                pin.Position = new Position(place.Latitude, place.Longitude);
                pin.Label = place.Name;
                avgLatitude += place.Latitude;
                avgLongitude += place.Longitude;
                EventMap.Pins.Add(pin);
            }

            if (_eventActivities.Count == 0)
            {
                avgLatitude /= _eventActivities.Count;
                avgLongitude /= _eventActivities.Count;
                EventMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(avgLatitude, avgLongitude), Distance.FromMeters(400)));
            }

            else
            {
                EventMap.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        new Position(53.118293, 23.149717), Distance.FromMeters(300)));
            }
        }
    }
}