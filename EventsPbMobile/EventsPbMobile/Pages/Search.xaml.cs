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
            Title = "Szukaj";
            events = new ObservableCollection<EventViewModel>();
            dataAccess = new EventsDataAccess();
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

        private async void OnTappedSearchedEvent(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem == null) return;
            //cast object to event
            var _event = e.SelectedItem as EventViewModel;
            if (_event == null || _event.Event.Active == false) return;

            //deselect item just in case
            ((ListView)sender).SelectedItem = null;
            var eventdetails = new EventDetails(_event.Event) { BindingContext = _event.Event };
            await Navigation.PushAsync(eventdetails);
        }
    }
}