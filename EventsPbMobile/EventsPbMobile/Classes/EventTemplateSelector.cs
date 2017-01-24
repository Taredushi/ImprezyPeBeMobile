using System;
using System.Diagnostics;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Classes
{
    public class EventTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LastEvent { get; set; }
        public DataTemplate ActiveEvent { get; set; }
        public DataTemplate UnactiveEvent { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var _event = (EventViewModel)item;
            if (_event.Event.Active && _event.Event.Date >= DateTime.Today)
                return ActiveEvent;
            return UnactiveEvent;
        }
    }
}
