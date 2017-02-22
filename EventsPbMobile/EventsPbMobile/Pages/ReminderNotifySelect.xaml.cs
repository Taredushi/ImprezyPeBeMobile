using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using EventsPbMobile.Classes;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class ReminderNotifySelect : ContentPage
    {
        private readonly EventsDataAccess db;

        public ReminderNotifySelect(Event e)
        {
            db = new EventsDataAccess();
            InitializeComponent();
            Title = "Ustaw powiadomienie";
            _Event = e;
           // InitSelectionCells();
            InitFavButton();
        }

        private Event _Event { get; set; }

        private void DisableSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return;
            ((ListView) sender).SelectedItem = null;
        }

/*        private void InitSelectionCells()
        {
            Debug.WriteLine("Count from remindersDB " + remindersFromDB.Count());
            foreach (var cell in times)
            foreach (var reminder in remindersFromDB)
            {
                if (reminder.NotificationTime==cell.ReminderTime)
                {
                    cell.Selected = true;
                }
            }

            TimeSelection.ItemsSource = times;
        }*/

        private void InitFavButton()
        {
            ToolbarItems.Add(new ToolbarItem("Zapisz", "save.png", () =>
            {
                
               
            }));
        }



        public class ReminderCell
        {
            public ReminderCell(int remindertime, bool selected, string textdisplayed)
            {
                ReminderTime = remindertime;
                Selected = selected;
                TextDisplayed = textdisplayed;
            }

            public int ReminderTime { get; }
            public bool Selected { get; set; }
            public string TextDisplayed { get; set; }
        }
    }
}