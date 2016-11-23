using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsPB.Model
{
    public class TodoListPageCs : ContentPage
    {
        public TodoListPageCs()
        {
            Title = "TodoList Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Todo list data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
