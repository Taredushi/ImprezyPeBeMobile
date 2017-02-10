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
        private readonly IQueryable<EventReminder> remindersFromDB;

        private readonly ObservableCollection<ReminderCell> times = new ObservableCollection<ReminderCell>
        {
            new ReminderCell(1, false, "Za jedną minutę"),
            new ReminderCell(2, false, "Za dwie minuty"),
            new ReminderCell(3, false, "Za trzy minuty")
        };

        public ReminderNotifySelect(Event e)
        {
            db = new EventsDataAccess();
            InitializeComponent();
            Title = "Ustaw powiadomienie";
            _Event = e;
            remindersFromDB = db.GetEventtReminders(e.EventId);
            InitSelectionCells();
            InitFavButton();
        }

        private Event _Event { get; set; }

        private void DisableSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null) return;
            ((ListView) sender).SelectedItem = null;
        }

        private void InitSelectionCells()
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
        }

        private void InitFavButton()
        {
            ToolbarItems.Add(new ToolbarItem("Zapisz", "save.png", () =>
            {
                
                db.SaveReminderStatusOfEvent(_Event,times);

                DisplayAlert("Sukces", "Pomyślnie zapisano powiadomienia", "OK");
                Navigation.PopAsync();
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