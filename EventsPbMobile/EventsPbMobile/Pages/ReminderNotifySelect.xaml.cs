using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class ReminderNotifySelect : ContentPage
    {
        private class ReminderCell
        {
            public string ReminderTime { get; }
            public bool Selected { get; }

            public ReminderCell(string remindertime, bool selected)
            {
                ReminderTime = remindertime;
                Selected = selected;
            }
        }

        private ObservableCollection<ReminderCell> times = new ObservableCollection<ReminderCell>()
        {
            new ReminderCell("1 min przed",false),
            new ReminderCell("2 min przed",false),
            new ReminderCell("3 min przed",false),
        };

        public ReminderNotifySelect()
        {
            InitializeComponent();
            Title = "Ustaw powiadomienie";
            
            TimeSelection.ItemsSource = times;
        }

        private void DisableSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}