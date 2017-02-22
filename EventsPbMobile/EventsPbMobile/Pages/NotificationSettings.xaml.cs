using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class NotificationSettings : ContentPage
    {
        /*private List<NotificationTime> times = new List<NotificationTime>()
        {
            new NotificationTime("Na godzinę przed", false),
            new NotificationTime("Na dzień przed", false),
            new NotificationTime("Na dwa dni przed", false)
        };*/
        public NotificationSettings()
        {
            InitializeComponent();
         //   NotificationTimesListView.ItemsSource = times;
            Title = "";
        }

        private void NotificationSwitch(object sender, ToggledEventArgs e)
        {
            NotificationTimesListView.IsVisible = e.Value;
        }

       /* private class NotificationTime
        {
            public NotificationTime(string notifytime, bool selected)
            {
                NotifyTime = notifytime;
                Selected = selected;
            }
            public string NotifyTime { get; set; }
            public bool Selected { get; set; }
        }*/

        private void SelectionChange(object sender, ToggledEventArgs e)
        {
            
        }

        private void DisableSelectionItem(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return; 
            ((ListView)sender).SelectedItem = null; 
        }
    }

    
}
