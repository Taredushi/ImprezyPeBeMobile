using System;
using Xamarin.Forms;
using MenuItem = EventsPbMobile.Models.MenuItem;

namespace EventsPbMobile.Pages
{
    public partial class MainMenu : MasterDetailPage
    {
        public MainMenu()
        {
            InitializeComponent();
            MenuDetail.ListView.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;

            if (item != null)
            {
                Detail = new NavigationPage((Page) Activator.CreateInstance(item.TargetType));
                var xx = new NavigationPage();
                MenuDetail.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}