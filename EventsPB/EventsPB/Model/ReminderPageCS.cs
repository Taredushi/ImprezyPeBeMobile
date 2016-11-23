using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventsPB.Model
{
    public class ReminderPageCS : ContentPage
    {
        public ReminderPageCS()
        {
            Title = "Reminder Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Reminder data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
