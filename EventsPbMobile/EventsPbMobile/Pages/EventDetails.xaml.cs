using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Controls;
using EventsPbMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails
    {
        private readonly EventsDataAccess _dataAccess;
        private readonly Event _event;
        private ToolbarItem _enableNotificationItem, _disableNotificationItem;

        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            _dataAccess = new EventsDataAccess();
            InitToolbarItems();
            InitFavButton();
            InitEventMap();
            GenerateContent();
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
            foreach (var activity in _event.Activities)
            {
                var pin = new Pin();
                var place = activity.Place;
                pin.Position = new Position(place.Latitude, place.Longitude);
                pin.Label = place.Name;
                avgLatitude += place.Latitude;
                avgLongitude += place.Longitude;
                EventMap.Pins.Add(pin);
            }

            if (_event.Activities.Count != 0)
            {
                avgLatitude /= _event.Activities.Count;
                avgLongitude /= _event.Activities.Count;
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

        private void GenerateContent()
        {
            foreach (var act in _event.Activities)
            {
                var separator = new Label(){HeightRequest = 2, BackgroundColor = Xamarin.Forms.Color.FromHex("#2d7a3a") };
                var btn = new ActivityBtn(act);
                ContentStackLayout.Children.Add(btn);
                ContentStackLayout.Children.Add(separator);
            }
        }

        private void ScrollView_OnScrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY >= this.ScrollView.ContentSize.Height - MapStack.Height - 100)
            {
                this.ScrollView.InputTransparent = true;
            }
            else
            {
                this.ScrollView.InputTransparent = false;
            }
        }
    }
}