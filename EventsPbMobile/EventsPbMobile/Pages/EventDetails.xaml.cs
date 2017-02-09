using System;
using System.Collections.ObjectModel;
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
                activities.Add(activity);

            EventPlaces.ItemsSource = activities;
        }

        private async void Counter()
        {
            var rt = _event.Date.Subtract(DateTime.Now);
            while (rt.TotalSeconds > 0)
            {
                rt = _event.Date.Subtract(DateTime.Now);
                TitleLabel.Text = "Start za: " + rt.Days + " dni, " + rt.Hours + ":" + rt.Minutes + ":" + rt.Seconds;
                await Task.Delay(250);
            }
        }

        private async void MapButton_OnClicked(object sender, EventArgs e)
        {
            var activities = _event.Activities;
            await Navigation.PushAsync(new EventMap(activities));
        }

        private void Notification_Clicked(object sender, EventArgs e)
        {
            if (Settings.IsToggled)
                App.Notification.ShowNotification("Nadchodzące wydarzenie", _event.Title);
        }

        private void InitFavButton()
        {
            ToolbarItems.Add(new ToolbarItem("Remind", "alert.png", () =>
            {
                Navigation.PushAsync(new ReminderNotifySelect());
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