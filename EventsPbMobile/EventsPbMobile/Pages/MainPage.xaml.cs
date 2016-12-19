using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var ev = new EventsDataAccess();
            EventsList.Header = "poprzednie wydarzenie";
            EventsList.ItemsSource = ev.GetEvents();
            CountDownTime();
        }

        private async void CountDownTime()
        {
            var eventTime = DateTime.Now.AddMonths(1);
            var remainingTime = eventTime - DateTime.Now;

            while (remainingTime.Seconds > 0)
            {
                remainingTime = eventTime - DateTime.Now;
                /* DaysLabel.Text = remainingTime.Days.ToString();
                 HoursLabel.Text = remainingTime.Hours.ToString();
                 MinutesLabel.Text = remainingTime.Minutes.ToString();
                 SecondsLabel.Text = remainingTime.Seconds.ToString();*/
                //countdown.Text = remainingTime.Seconds.ToString();
                await Task.Delay(1000);

                try
                {
                    var currentResource = Application.Current.Resources["CountdownLabel"];
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void BottomTitle_OnClicked(object sender, EventArgs e)
        {
            var da = new EventsDataAccess();
            da.AddEvents();
            da.SaveDataToDb();
            var test = da.GetEvents();
        }

        private async void OnEventSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //checking if null because this event is triggered
            //when item is selected, but also when item is diselected (then its null)
            if (e.SelectedItem != null)
            {
                //cast object to event
                var _event = e.SelectedItem as Event;
                Debug.WriteLine(sender.ToString());

                //deselect item just in case
                ((ListView)sender).SelectedItem = null;
                await Navigation.PushAsync(new EventDetails());
            }
        }

        private void XButton_OnClicked(object sender, EventArgs e)
        {
            var da = new EventsDataAccess();
            da.AddEvents();
            da.SaveDataToDb();
        }
    }
}