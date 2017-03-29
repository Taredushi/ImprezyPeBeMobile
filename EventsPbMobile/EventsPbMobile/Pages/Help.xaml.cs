using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private void Button_GoToApplicationClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
                Navigation.RemovePage(this);
            }
            catch (Exception)
            {
                Application.Current.MainPage = new MainMenu();
                Navigation.RemovePage(this);
            }

        }
    }
}
