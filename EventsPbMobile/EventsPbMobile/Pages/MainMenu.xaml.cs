using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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

            if (item == null) return;

            Detail = new NavigationPage((Page) Activator.CreateInstance(item.TargetType));
            // MenuDetail.ListView.SelectedItem = null;
            IsPresented = false;
        }

        public void SetPage(Type name)
        {
            List<MenuItem> list = (from object obj in items select obj as MenuItem).ToList();

            var item = list.First(x => x.TargetType == name);

            if (item == null) return;
            MenuDetail.ListView.SelectedItem = item;
        }
        
    }
}