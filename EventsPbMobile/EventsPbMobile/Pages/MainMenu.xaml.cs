using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using MenuItem = EventsPbMobile.Models.MenuItem;

namespace EventsPbMobile.Pages
{
    public partial class MainMenu : MasterDetailPage
    {
        private IEnumerable items;
        public MainMenu()
        {
            InitializeComponent();
            MenuDetail.ListView.ItemSelected += OnItemSelected;
            items = MenuDetail.ListView.ItemsSource;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItem;
            
            if (item != null)
            {
                
                Detail = new NavigationPage((Page) Activator.CreateInstance(item.TargetType));
               // MenuDetail.ListView.SelectedItem = null;
                IsPresented = false;
                Debug.WriteLine(Application.Current.MainPage.Title);
            }
        }

        private void InitSelection()
        {
           
        }
        
    }
}