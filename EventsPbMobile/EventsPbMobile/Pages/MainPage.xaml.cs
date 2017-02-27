using System;
using System.ComponentModel;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private readonly EventsDataAccess _dataAccess;
        private bool isLoading;

        public MainPage()
        {
            InitializeComponent();
            //App.Notification.StartService();
            BindingContext = this;
            _dataAccess = new EventsDataAccess();
            // _dataAccess.DeleteDatabase();
            EventsList.ItemsSource = _dataAccess.Events;
            InitializeSeachButton();
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

        protected override async void OnAppearing()
        {
            EventsList.IsRefreshing = true;
            var t = await RefreshDatabase();
            EventsList.IsRefreshing = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            GC.Collect();
        }

        private void InitializeSeachButton()
        {
            ToolbarItems.Add(new ToolbarItem("Search", "search.png", () => { Navigation.PushAsync(new Search()); }));
        }

        private async void Events_PullToRefreshAction(object sender, EventArgs e)
        {
            var eventlist = sender as ListView;
            var t = await RefreshDatabase();
            eventlist.IsRefreshing = false;
        }

        private async Task<bool> RefreshDatabase()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Błąd", "Nie masz połączenia z internetem", "OK");
                return false;
            }
            var photos = await _dataAccess.SavePhotosToDb();
            var places = await _dataAccess.SavePlacesToDb();
            var events = await _dataAccess.SaveEventsToDb();
            var activities = await _dataAccess.SaveActivitiesToDb();
            var photoevents = await _dataAccess.SavePhotoEventsToDb();

            return true;
        }
    }
}