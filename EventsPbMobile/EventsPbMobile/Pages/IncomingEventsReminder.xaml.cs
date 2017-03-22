using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class IncomingEventsReminder : ContentPage
    {
        private readonly EventsDataAccess db;

        public IncomingEventsReminder()
        {
            InitializeComponent();
            Title = "Zapisane wydarzenia";
            db = new EventsDataAccess();
            EventsWithSetReminder.ItemsSource = db.GetEventsWithSetReminder();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PushAsync(new MainPage(), true);
            Navigation.RemovePage(this);
            return true;
        }

		private async void OnTappedSearchedEvent(object sender, SelectedItemChangedEventArgs e)
		{
			//checking if null because this event is triggered
			//when item is selected, but also when item is diselected (then its null)
			if (e.SelectedItem == null) return;
			//cast object to event
			var _event = e.SelectedItem as Event;
			if (_event == null || _event.Active == false) return;

			//deselect item just in case
			((ListView)sender).SelectedItem = null;
			var eventdetails = new EventDetails(_event) { BindingContext = _event };
			await Navigation.PushAsync(eventdetails);
		}
    }
}