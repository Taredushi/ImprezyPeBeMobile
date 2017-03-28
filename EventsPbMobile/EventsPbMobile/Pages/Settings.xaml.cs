using System;
using System.Collections.Generic;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class Settings : ContentPage
    {
        private readonly List<NotificationTime> _times = new List<NotificationTime>()
        {
            new NotificationTime("Na godzinę przed", false),
            new NotificationTime("Na dzień przed", false),
            new NotificationTime("Na dwa dni przed", false)
        };

        private Models.Settings _settings;
        private readonly EventsDataAccess _db;
        private IEnumerable<Event> _events;
        public Settings()
        {
            InitializeComponent();
            _db = new EventsDataAccess();
            InitializeSettings();
            _events = _db.GetEventsWithSetReminder();
            NotificationTimesListView.ItemsSource = _times;
        }

        public static bool IsToggled { get; private set; }

        private void PushNotificationsProperty(object sender, ToggledEventArgs e)
        {
            IsToggled = e.Value;
        }

        private void InitializeSettings()
        {
            _settings = _db.GetSettings();

            SettingsEnabled.IsToggled = _settings.NotificationsEnabled;
            NotificationTimesListView.IsVisible = _settings.NotificationsEnabled;

            _times[0].Selected = _settings.Notify1HBefore;
            _times[1].Selected = _settings.Notify1DBefore;
            _times[2].Selected = _settings.Notify2DBefore;

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
            return true;
        }

        private void SelectionChange(object sender, ToggledEventArgs e)
        {
            var settings = _db.GetSettings();

            var database = _db.GetDbInstance();

            database.Write(() =>
            {
                settings.Notify1HBefore = _times[0].Selected;
                settings.Notify1DBefore = _times[1].Selected;
                settings.Notify2DBefore = _times[2].Selected;

            });

            UpdateAlarms();
        }

        private void NotificationSwitch(object sender, ToggledEventArgs e)
        {
            NotificationTimesListView.IsVisible = e.Value;
            var database = _db.GetDbInstance();

            database.Write(() =>
            {
                _settings.NotificationsEnabled = e.Value;
            });

            UpdateAlarms();

        }

        private void UpdateAlarms()
        {
            if (_settings.NotificationsEnabled)
            {
                AlarmNotification.SetAlarms();
            }
            else
            {
                AlarmNotification.DisaBleAlarms();
            }
        }

        private class NotificationTime
        {
            public NotificationTime(string notifytime, bool selected)
            {
                NotifyTime = notifytime;
                Selected = selected;
            }

            public string NotifyTime { get; set; }
            public bool Selected { get; set; }
        }
    }
}