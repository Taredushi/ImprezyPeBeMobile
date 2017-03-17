using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsPbMobile.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Help : CarouselPage
    {
        public Help()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
            return true;
        }
    }
}
