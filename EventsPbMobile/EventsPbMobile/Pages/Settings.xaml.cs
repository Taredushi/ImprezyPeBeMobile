using System;
using System.Collections.Generic;
using EventsPbMobile.Classes;
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
        private EventsDataAccess db;
        public Settings()
        {
            InitializeComponent();
            db = new EventsDataAccess();
            InitializeSettings();
            NotificationLabelInit();
            NotificationTimesListView.ItemsSource = _times;
        }

        public static bool IsToggled { get; private set; }

        private void PushNotificationsProperty(object sender, ToggledEventArgs e)
        {
            IsToggled = e.Value;
        }

        private void InitializeSettings()
        {
            _settings = db.GetSettings();

            SettingsEnabled.IsToggled = _settings.NotificationsEnabled;
            NotificationTimesListView.IsVisible = _settings.NotificationsEnabled;

            _times[0].Selected = _settings.Notify1HBefore;
            _times[1].Selected = _settings.Notify1DBefore;
            _times[2].Selected = _settings.Notify2DBefore;

        }

        private void NotificationLabelInit()
        {
            /*NotificationLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { Navigation.PushAsync(new NotificationSettings()); })
            });*/
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
            return true;
        }

        private void ListViewItemSwitchChanged(object sender, SelectedItemChangedEventArgs e)
        {
           
        }

        private void SelectionChange(object sender, ToggledEventArgs e)
        {
            var settings = db.GetSettings();

            var database = db.GetDbInstance();

            database.Write(() =>
            {
                settings.Notify1HBefore = _times[0].Selected;
                settings.Notify1DBefore = _times[1].Selected;
                settings.Notify2DBefore = _times[2].Selected;

            });
        }

        private void NotificationSwitch(object sender, ToggledEventArgs e)
        {
            NotificationTimesListView.IsVisible = e.Value;
            var database = db.GetDbInstance();

            database.Write(() =>
            {
                _settings.NotificationsEnabled = e.Value;
            });

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