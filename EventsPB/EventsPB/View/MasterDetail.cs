using System;
using EventsPB.Model;
using Xamarin.Forms;

namespace EventsPB.View
{
    public class MasterDetail : MasterDetailPage
    {
        MasterPageCs masterPage;

        public MasterDetail()
        {
            MasterBehavior = MasterBehavior.Popover;
            var content = new ContentPage
            {
                Title = "PBEvents",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Todo - wydarzenia na pb"
                        }
                    }
                }
            };

            masterPage = new MasterPageCs();
            Master = masterPage;
            Detail = new NavigationPage(content);
            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HamburgerItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }

    }
}
