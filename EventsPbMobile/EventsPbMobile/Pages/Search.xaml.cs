using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class Search : ContentPage
    {
        private readonly EventsDataAccess dataAccess;
        private readonly ObservableCollection<EventViewModel> events;

        public Search()
        {
            InitializeComponent();
            events = new ObservableCollection<EventViewModel>();
            dataAccess = new EventsDataAccess();
            NavigationPage.SetHasNavigationBar(this, false);
            ImageTapInit();
            SearchedEvents.ItemsSource = events;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == "")
            {
                events.Clear();
                return;
            }
            var list = dataAccess.Events.Where(x => x.Event.Title.Contains(e.NewTextValue));
            events.Clear();

            foreach (var element in list)
                events.Add(element);
        }

        private void OnSearch(object sender, EventArgs e)
        {
        }

        private void ImageTapInit()
        {
            var imageTappedRecogniser = new TapGestureRecognizer();
            imageTappedRecogniser.Tapped += (s, e) => { Navigation.PopAsync(true); };
            BackArrow.GestureRecognizers.Add(imageTappedRecogniser);
        }

        private void OnTappedSearchedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            Debug.WriteLine("Check if works");
        }
    }
}