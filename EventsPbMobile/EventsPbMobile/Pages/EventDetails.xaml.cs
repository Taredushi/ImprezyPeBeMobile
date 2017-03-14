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
        private ToolbarItem _enableNotificationItem, _disableNotificationItem;
        private readonly ObservableCollection<Activity> evenActivities;

        public EventDetails(Event e)
        {
            _event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
            _dataAccess = new EventsDataAccess();
            evenActivities = new ObservableCollection<Activity>();
            var activities = _dataAccess.GetActivitiesForEvent(e.EventId);
            evenActivities.Clear();

            foreach (var activity in activities)
            {
                var ac = new Activity(activity);
                ac.Place = _dataAccess.GetPlace(ac.PlaceID);
                evenActivities.Add(ac);
            }
            InitToolbarItems();
            InitFavButton();
            EventPlaces.ItemsSource = evenActivities;
           
        }

        private async void Counter()
        {
            var start = _event.StartDate.Subtract(DateTimeOffset.Now);
            var end = _event.EndDate.Subtract(DateTimeOffset.Now);

            string hours, minutes, seconds;
            if(start.TotalSeconds>0) 
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
            {
                while (end.TotalSeconds > 0)
                {
                    TitleLabel.Text = "Event trwa!";
                    await Task.Delay(10000);
                }
            }
            else
            {
                TitleLabel.Text = "Event zakończony";
            }
            
        }

        private async void MapButton_OnClicked(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;

            if (stack[stack.Count - 1].GetType() != typeof(EventMap))
                await Navigation.PushAsync(new EventMap(evenActivities, Title));
        }

        private async void EventInDepartamentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e==null || ((ListView)sender).SelectedItem == null) return;
            
            var activity = e.SelectedItem as Activity;
            var place = _dataAccess.GetPlace(activity.PlaceID);
            ((ListView) sender).SelectedItem = null;
            await Navigation.PushAsync(new EventDepartamentDetails(Title, activity, place));
        }

        private void InitToolbarItems()
        {
            _enableNotificationItem = new ToolbarItem("Remind", "notiactive.png", async () =>
            {
                var answer = await DisplayAlert("Powiadomienie", "Czy chcesz ustawić powiadomienie na to wydarzenie?",
                    "Tak", "Nie");

                if (!answer) return;

                _dataAccess.SaveEventWithSetReminder(_event.EventId);
                InitFavButton();
            });

            _disableNotificationItem = new ToolbarItem("Remind", "notidisabled.png", async () =>
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
    }
}