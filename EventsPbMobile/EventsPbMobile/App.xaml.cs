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
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static INotification Notification { get; private set; }
        public static IDownloadManager DownloadManager { get; private set; }
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

        public static void InitDownload(IDownloadManager manager)
        {
            App.DownloadManager = manager;
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
