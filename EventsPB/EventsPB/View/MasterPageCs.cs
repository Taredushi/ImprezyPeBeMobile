using System.Collections.Generic;
using EventsPB.Model;
using Xamarin.Forms;

namespace EventsPB.View
{
    public class MasterPageCs : ContentPage
    {
        public ListView ListView { get; }

        public MasterPageCs()
        {
            var items = new List<HamburgerItem>
            {
                new HamburgerItem
                {
                    Title = "Contacts",
                    IconSource = "contacts.png",
                    TargetType = typeof(ContactsPageCs)
                },
                new HamburgerItem
                {
                    Title = "TodoList",
                    IconSource = "todo.png",
                    TargetType = typeof(TodoListPageCs)
                },
                new HamburgerItem
                {
                    Title = "Reminders",
                    IconSource = "reminders.png",
                    TargetType = typeof(ReminderPageCS)
                }
            };

            ListView = new ListView
            {
                ItemsSource = items,
                ItemTemplate = new DataTemplate(() => {
                    var imageCell = new ImageCell();
                    imageCell.SetBinding(TextCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
                    return imageCell;
                }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None
            };

            Padding = new Thickness(0, 40, 0, 0);
            Icon = "hamburger.png";
            Title = "Personal Organiser";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                ListView
            }
            };
        }
    }
}
