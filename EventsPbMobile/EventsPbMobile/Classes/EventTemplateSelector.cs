using System;
using EventsPbMobile.Models;
using Xamarin.Forms;

namespace EventsPbMobile.Classes
{
    public class EventTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ActiveEvent { get; set; }
        public DataTemplate UnactiveEvent { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var _event = (EventViewModel) item;
            return _event.Event.Active ? ActiveEvent : UnactiveEvent;
        }
    }
}