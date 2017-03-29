using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using EventsPbMobile.Classes;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsPbMobile.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Informer : ContentPage
    {
        private string informatorInfo = "Informator należy pobrać. Wykorzystując transfer danych mogą zostać naliczone opłaty zgodnie z cennikiem operatora. " +
                                   "Pobrać informator?";

        private string noInternetError = "Do pobrania informatora wymagane jest połączenie z internetem.";

        public Informer()
        {
            //InitializeComponent();
            Start();
        }

        /* protected override void OnAppearing()
         {
             base.OnAppearing();
             Navigation.PushAsync(new MainPage());
             OpenFileInBrowser();
             Navigation.RemovePage(this);
         }*/

        private async void Start()
        {
            if (!DependencyService.Get<IPdfViewer>().FileExists("informatorpb.pdf"))
            {
                if (await DisplayWarning())
                {
                    OnDisappearing();
                    await Task.Run(() =>
                    {
                        DependencyService.Get<IDownloadManager>()
                            .Download(
                                "http://pb.edu.pl/wp-content/uploads/2013/04/pdf_compresor_2017_01_17_WWW_1_informator-2017-2018_MW-min.pdf",
                                "informatorpb.pdf");
                        bool exists = false;

                        do
                        {
                            exists = DependencyService.Get<IPdfViewer>().FileExists("informatorpb.pdf");

                        } while (!exists);

                        DependencyService.Get<IPdfViewer>().View("informatorpb.pdf");
                    });
                    return;
                }
                OnDisappearing();
            }
            else
            {
                DependencyService.Get<IPdfViewer>().View("informatorpb.pdf");
            }
            
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RemovePageFromStack();
        }

        private void RemovePageFromStack()
        {
            var parent = this.Parent;
            while (!(parent is MainMenu))
            {
                if (parent.Parent == null) break;
                parent = parent.Parent;
            }
            if (parent is MainMenu)
            {
                (parent as MainMenu).SetPage(typeof(MainPage));
            }
        }

        private async Task<bool> DisplayWarning()
        {
            var status = await DisplayAlert("Informator", informatorInfo, "Tak", "Nie");
            if (!status) return false;

            if (CrossConnectivity.Current.IsConnected) return true;
            await DisplayAlert("Informator", noInternetError, "Zamknij");

            return false;
        }
    }

}
