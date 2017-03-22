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
           // InitializeComponent();
            LoadTestFile();
            OpenFileInBrowser();
        }

        /* protected override void OnAppearing()
         {
             base.OnAppearing();
             Navigation.PushAsync(new MainPage());
             OpenFileInBrowser();
             Navigation.RemovePage(this);
         }*/

        private void LoadTestFile()
        {
            DependencyService.Get<IDownloadManager>().Download("http://pb.edu.pl/wp-content/uploads/2013/04/informator-2015-2016.pdf", "informatorpb.pdf");
        }

        private void OpenFileInBrowser()
        {
            DependencyService.Get<IPdfViewer>().View("informatorpb.pdf");
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
