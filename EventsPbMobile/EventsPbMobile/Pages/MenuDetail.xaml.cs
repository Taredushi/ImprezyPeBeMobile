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
                    IconSource = "home.png",
                    TargetType = typeof(MainPage)
                },
                new Models.MenuItem()
                {
                    Title = "Mapa",
                    IconSource = "map.png",
                    TargetType = typeof(Map)
                },
                new Models.MenuItem()
                {
                    Title = "Zapisane Eventy",
                    IconSource = "incomingevents.png",
                    TargetType = typeof(IncomingEventsReminder)
                },
                new Models.MenuItem()
                {
                    Title = "Przydatne Linki",
                    IconSource = "links.png",
                    TargetType = typeof(UsefulLinks)
                },
                new Models.MenuItem()
                {
                    Title = "Informator",
                    IconSource = "links.png",
                    TargetType = typeof(Informer)
                },
                new Models.MenuItem()
                {
                    Title = "Kontakt",
                    IconSource = "contact.png",
                    TargetType = typeof(ContactsPage)
                },
                new Models.MenuItem()
                {
                    Title = "Pomoc",
                    IconSource = "help.png",
                    TargetType = typeof(Help)
                },
                new Models.MenuItem()
                {
                    Title = "Ustawienia",
                    IconSource = "settings.png",
                    TargetType = typeof(Settings)
                },
                new Models.MenuItem()
                {
                    Title = "O Aplikacji",
                    IconSource = "about.png",
                    TargetType = typeof(About)
                }
            };

            MenuListView.ItemsSource = masterPageItems;
        }
    }
}
