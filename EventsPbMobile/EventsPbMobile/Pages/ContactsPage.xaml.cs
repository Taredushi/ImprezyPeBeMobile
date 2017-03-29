using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EventsPbMobile.Pages
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            var parent = this.Parent;
            while (!(parent is MainMenu))
            {
                if (parent.Parent == null) break;
                parent = parent.Parent;
            }
            if (parent is MainMenu)
            {
                (parent as MainMenu).SetPage(typeof(MainPage));
                return true;
            }
            return false;
        }

    }
}
