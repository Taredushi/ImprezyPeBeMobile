using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Classes;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class MenuDetail : ContentPage
    {
        public ListView ListView => MenuListView;

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
                    IconSource = "ic_home_white_36dp.png",
                    TargetType = typeof(MainPage)
                },
                new Models.MenuItem()
                {
                    Title = "Mapa",
                    IconSource = "ic_map_white_36dp.png",
                    TargetType = typeof(Map)
                },
                new Models.MenuItem()
                {
                    Title = "Zapisane imprezy",
                    IconSource = "ic_alarm_white_36dp.png",
                    TargetType = typeof(IncomingEventsReminder)
                },
                new Models.MenuItem()
                {
                    Title = "Informator",
                    IconSource = "ic_picture_as_pdf_white_36dp.png",
                    TargetType = typeof(Informer)
                },
                new Models.MenuItem()
                {
                    Title = "Kontakt",
                    IconSource = "ic_contact_mail_white_36dp.png",
                    TargetType = typeof(ContactsPage)
                },
                new Models.MenuItem()
                {
                    Title = "Pomoc",
                    IconSource = "ic_help_white_36dp.png",
                    TargetType = typeof(Help)
                },
                new Models.MenuItem()
                {
                    Title = "Ustawienia",
                    IconSource = "ic_settings_white_36dp.png",
                    TargetType = typeof(Settings)
                },
                new Models.MenuItem()
                {
                    Title = "O Aplikacji",
                    IconSource = "ic_info_white_36dp.png",
                    TargetType = typeof(About)
                }
            };

            MenuListView.ItemsSource = masterPageItems;
            
        }
    }
}