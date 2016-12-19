using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class EventDetails : ContentPage
    {
        private Event _event;
        public EventDetails(Event e)
        {
            this._event = e;
            InitializeComponent();
            Title = e.Title;
            Counter();
        }

        private async void Counter()
        {
            
        }
    }
}
