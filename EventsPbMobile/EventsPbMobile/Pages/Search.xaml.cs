using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class Search : ContentPage
    {
        private readonly ObservableCollection<Activity> _activities;
        private readonly IList<Activity> _activitiesQueryable;
        private readonly EventsDataAccess _dataAccess;
        public Search()
        {
            InitializeComponent();
            Title = "Szukaj";
            _activities = new ObservableCollection<Activity>();
            _dataAccess = new EventsDataAccess();
            _activitiesQueryable = _dataAccess.GetActivities();

            SearchedActivities.ItemsSource = _activities;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _activities.Clear();
            if (e.NewTextValue == "")
            {
                return;
            }

            var activitiesList =
                _activitiesQueryable.Where(
                    x =>
                        x.Title.ToLower().Contains(e.NewTextValue.ToLower()) ||
                        x.Text.ToLower().Contains(e.NewTextValue.ToLower()));

            foreach (var activity in activitiesList)
                _activities.Add(activity);
        }

        private async void OnTappedSearchedActivity(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem == null) return;
            //cast object to event
            Activity activity = e.SelectedItem as Activity;
            if (activity == null) return;

            //deselect item just in case
            ((ListView) sender).SelectedItem = null;

            var eventtitle = _dataAccess.GetEventTitle(activity.EventID);
            var eventdetails = new EventDepartamentDetails(eventtitle, activity) {BindingContext = activity.Event};
            await Navigation.PushAsync(eventdetails);
        }

    }
}