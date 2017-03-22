using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EventsPbMobile
{
	public partial class AdvancedSearch : ContentPage
	{

		public bool DateSearch { get; set; }
		public bool PlaceSearch { get; set; }
		public bool TextSearch { get; set; }
		public bool TitleSearch { get; set; }

		void BackButton_Clicked(object sender, System.EventArgs e)
		{
			Save();
			Navigation.PopModalAsync();
		}

		protected override bool OnBackButtonPressed()
		{
			Save();
			Navigation.PopModalAsync();
			return true;
		}
		//void Date_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		//{
		//	AdvancedSearchOptions.Date = !AdvancedSearchOptions.Date;
		//}

		//void Place_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		//{
		//	AdvancedSearchOptions.Place = !AdvancedSearchOptions.Place;
		//}

		//void Text_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		//{
		//	AdvancedSearchOptions.Text = !AdvancedSearchOptions.Text;
		//}

		//void Title_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		//{
		//	AdvancedSearchOptions.Title = !AdvancedSearchOptions.Title;
		//}

		public AdvancedSearch()
		{
			InitializeComponent();
			DateSwitch.IsToggled = AdvancedSearchOptions.Date;
			TextSwitch.IsToggled = AdvancedSearchOptions.Text;
			TitleSwitch.IsToggled = AdvancedSearchOptions.Title;
			PlaceSwitch.IsToggled = AdvancedSearchOptions.Place;
		}

		private void Save()
		{
			AdvancedSearchOptions.Date = DateSwitch.IsToggled;
			AdvancedSearchOptions.Place = PlaceSwitch.IsToggled;
			AdvancedSearchOptions.Text = TextSwitch.IsToggled;
			AdvancedSearchOptions.Title = TitleSwitch.IsToggled;
		}
	}
}
