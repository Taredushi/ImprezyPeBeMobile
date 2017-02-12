using EventsPbMobile.Classes;
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
    }
}