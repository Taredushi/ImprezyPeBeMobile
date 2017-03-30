using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using EventsPbMobile.Pages;
using Xamarin.Forms;

namespace EventsPbMobile.Controls
{
    public partial class ActivityBtn : ContentView
    {
        public Activity Activity { get; set; }
        public string Title { get; set; }
        public string PlaceAndDate { get; set; }
        public ActivityBtn(Activity activity)
        {
            Activity = activity;
            Title = activity.Title;
            PlaceAndDate = activity.PlaceAndDate;
            InitializeComponent();
            BindingContext = this;
        }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new EventDepartamentDetails(Activity));
        }
    }
}
