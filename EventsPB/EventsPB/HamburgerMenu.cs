using EventsPB.Model;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EventsPB
{
    public class HamburgerMenu : ContentPage
    {
        public ListView ListView
        {
            get { return listView; }
        }

        ListView listView;

        public HamburgerMenu()
        {
            var masterPageItems = new List<HamburgerItem>();
            masterPageItems.Add(new HamburgerItem
            {
                Title = "Contacts",
                IconSource = "contacts.png",
                TargetType = typeof(ContactsPageCs)
            });
            masterPageItems.Add(new HamburgerItem
            {
                Title = "TodoList",
                IconSource = "todo.png",
                TargetType = typeof(TodoListPageCs)
            });
            masterPageItems.Add(new HamburgerItem
            {
                Title = "Reminders",
                IconSource = "reminders.png",
                TargetType = typeof(ReminderPageCS)
            });

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imageCell = new ImageCell();
                    imageCell.SetBinding(TextCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
                    return imageCell;
                }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None
            };

            //Padding = new Thickness(0, 40, 0, 0);
            Icon = "hamburger.png";
            Title = "Personal Organiser";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    listView
                }
            };
        }
    }
}
