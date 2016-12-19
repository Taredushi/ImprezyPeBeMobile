using System;
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
            var _event = (Event)item;
            if (_event.Viewable && _event.Date >= DateTime.Today)
                return ActiveEvent;
            return UnactiveEvent;
        }
    }
}
