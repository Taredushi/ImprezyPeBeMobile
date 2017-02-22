using System.Collections.Generic;
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
        public Settings()
        {
            InitializeComponent();
            NotificationLabelInit();
            NotificationTimesListView.ItemsSource = _times;
        }

        public static bool IsToggled { get; private set; }

        private void PushNotificationsProperty(object sender, ToggledEventArgs e)
        {
            IsToggled = e.Value;
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

        private void DisableSelectionItem(object sender, SelectedItemChangedEventArgs e)
        {
           
        }

        private void SelectionChange(object sender, ToggledEventArgs e)
        {
            
        }

        private void NotificationSwitch(object sender, ToggledEventArgs e)
        {
            NotificationTimesListView.IsVisible = e.Value;
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