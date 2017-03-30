using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage
    {
        private readonly EventsDataAccess _dataAccess;

        public MainPage()
        {
            InitializeComponent();
            // App.Notification.StartService();
            BindingContext = this;
            _dataAccess = new EventsDataAccess();
            // _dataAccess.DeleteDatabase();
            CheckIfDbIsNull();
            InitializeSeachButton();
            AlarmNotification.SetAlarms();
        }


        private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem == null) return;
            //cast object to event
            var _event = e.SelectedItem as EventViewModel;
            if (_event == null || _event.Event.Active == false) return;

            //deselect item just in case
            ((ListView) sender).SelectedItem = null;
            var eventdetails = new EventDetails(_event.Event) {BindingContext = _event.Event};

            //prevent double tapping before opening another page
            //double tap causes double push of page onto navigation stack
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(EventDetails))
                await Navigation.PushAsync(eventdetails);
        }

        private async void CheckIfDbIsNull()
        {
            if (!_dataAccess.Events.Any())
            {
                EventsList.IsRefreshing = true;
                 await RefreshDatabase();
            }
            EventsList.ItemsSource = _dataAccess.Events;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshAfterPeriodOfTime();
        }

        private void InitializeSeachButton()
        {
            ToolbarItems.Add(new ToolbarItem("Search", "ic_search_white_36dp.png", () => { Navigation.PushAsync(new Search()); }));
        }

        private async Task<bool> RefreshAfterPeriodOfTime()
        {
            var settings = _dataAccess.GetSettings();
            var lastrefresh = DateTimeOffset.Now.Subtract(settings.LastRefreshDate);

            if (lastrefresh.TotalHours < 5) return true;

            await RefreshDatabase();

            var stng = new Models.Settings(settings) {LastRefreshDate = DateTimeOffset.Now};
            _dataAccess.SaveSettings(stng);
            return true;
        }

        private async void Events_PullToRefreshAction(object sender, EventArgs e)
        {
            var eventlist = sender as ListView;

            if (eventlist == null) return;
            await RefreshDatabase();

        }

        private async Task<bool> RefreshDatabase()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Błąd", "Nie masz połączenia z internetem", "OK");
                EventsList.IsRefreshing = false;
                return false;
            }
            try
            {
                await _dataAccess.SaveEventsToDb();
                var settings = _dataAccess.GetSettings();
                var stngs = new Models.Settings(settings) {LastRefreshDate = DateTimeOffset.Now};
                _dataAccess.SaveSettings(stngs);
                EventsList.IsRefreshing = false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + " " + e.StackTrace +  " "+ e.Source);
                await DisplayAlert("Błąd", "Wystąpił błąd przy pobieraniu danych lub zostało utracone połączenie z Internetem", "OK");
                EventsList.IsRefreshing = false;
            }
            return true;
        }
    }
}