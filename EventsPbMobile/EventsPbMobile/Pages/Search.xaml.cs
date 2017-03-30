using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class Search : ContentPage
    {
        private ObservableCollection<Activity> _activities;
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

			var result = new List<Activity>();

			if (AdvancedSearchOptions.Date)
			{
				result = result.Union(SearchByDate(e.NewTextValue.ToLower())).ToList();
			}
			if (AdvancedSearchOptions.Place)
			{
				result = result.Union(SearchByPlace(e.NewTextValue.ToLower())).ToList();
			}
			if (AdvancedSearchOptions.Text)
			{
				result = result.Union(SearchByText(e.NewTextValue.ToLower())).ToList();
			}
			if (AdvancedSearchOptions.Title)
			{
				result = result.Union(SearchByTitle(e.NewTextValue.ToLower())).ToList();
			}


			foreach (var activity in result)
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
            var eventdetails = new EventDepartamentDetails(activity) {BindingContext = activity.Event};
            await Navigation.PushAsync(eventdetails);
        }

		async void AdvancedSearch_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new AdvancedSearch());
		}

		private ICollection<Activity> SearchByDate(string searchstring)
		{
			return _activitiesQueryable.Where(
				x => x.StartHour.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture).ToLower().Contains(searchstring) || 
				x.StartHour.Date.DayOfWeek.ToString().ToLower().Contains(searchstring) ||
				x.StartHour.Date.ToString("MMMM").ToLower().Contains(searchstring)
			).Distinct().ToList();
		}

		private ICollection<Activity> SearchByPlace(string searchstring)
		{
			return _activitiesQueryable.Where(
				x => x.Place.Name.ToLower().Contains(searchstring)).Distinct().ToList();
		}

		private ICollection<Activity> SearchByText(string searchstring)
		{
			return _activitiesQueryable.Where(
				x =>x.Text.ToLower().Contains(searchstring)).Distinct().ToList();
		}

		private ICollection<Activity> SearchByTitle(string searchstring)
		{
			return _activitiesQueryable.Where(
				x => x.Title.ToLower().Contains(searchstring)).Distinct().ToList();
		}

    }
}