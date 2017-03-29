using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EventsPbMobile.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventsPbMobile.Pages
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Informer : ContentPage
    {
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

        private void Start()
        {
            if (!DependencyService.Get<IPdfViewer>().FileExists("informatorpb.pdf"))
            {
                DependencyService.Get<IDownloadManager>().Download("http://pb.edu.pl/wp-content/uploads/2013/04/pdf_compresor_2017_01_17_WWW_1_informator-2017-2018_MW-min.pdf", "informatorpb.pdf");
                Task.Run(() =>
                {
                    bool exists = false;

                    do
                    {
                        exists = DependencyService.Get<IPdfViewer>().FileExists("informatorpb.pdf");

                    } while (!exists);

                    DependencyService.Get<IPdfViewer>().View("informatorpb.pdf");

                });
            }
            else
            {
                DependencyService.Get<IPdfViewer>().View("informatorpb.pdf");
            }
            RemovePageFromStack();
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
            Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }

}
