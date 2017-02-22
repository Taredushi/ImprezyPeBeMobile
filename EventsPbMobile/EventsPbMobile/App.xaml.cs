using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventsPbMobile.Classes;
using EventsPbMobile.Pages;
using Xamarin.Forms;

namespace EventsPbMobile
{
    public partial class App : Application
    {
        public static INotification Notification { get; private set; }
        private EventsDataAccess _dataAccess;
        public App()
        {
            InitializeComponent();
            _dataAccess = new EventsDataAccess();
            InitNotificationSettings();
            MainPage = new MainMenu();
        }

        private void InitNotificationSettings()
        {
            
        }

        public static void Init(INotification notification)
        {
            App.Notification = notification;
        }
        public static Page GetMainPage()
        {
            return new NavigationPage(new MainMenu());
        }

        protected override void OnStart()
        {

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
        
    }
}
