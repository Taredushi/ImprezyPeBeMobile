using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MenuDetail : ContentPage
    {
        public ListView ListView { get { return MenuListView; } }

        public MenuDetail()
        {
            InitializeComponent();
            SetMenuItems();
        }

        private void SetMenuItems()
        {
            var masterPageItems = new List<Models.MenuItem>()
            {
                new Models.MenuItem()
                {
                    Title = "Strona Główna",
                    IconSource = "main.png",
                    TargetType = typeof(MainPage)
                },
                new Models.MenuItem()
                {
                    Title = "Mapa",
                    IconSource = "map.png",
                    //TargetType = typeof(ReminderPage)
                },
                new Models.MenuItem()
                {
                    Title = "Przydatne Linki",
                    IconSource = "map.png",
                    //TargetType = typeof(ReminderPage)
                },
                new Models.MenuItem()
                {
                    Title = "Kontakt",
                    IconSource = "contacts.png",
                    TargetType = typeof(ContactsPage)
                },
                new Models.MenuItem()
                {
                    Title = "Pomoc",
                    IconSource = "help.png",
                    //TargetType = typeof(ReminderPage)
                },
                new Models.MenuItem()
                {
                    Title = "Ustawienia",
                    IconSource = "settings.png",
                    //TargetType = typeof(ReminderPage)
                },
                new Models.MenuItem()
                {
                    Title = "O Aplikacji",
                    IconSource = "info.png",
                    //TargetType = typeof(ReminderPage)
                }
            };

            MenuListView.ItemsSource = masterPageItems;
        }
    }
}
