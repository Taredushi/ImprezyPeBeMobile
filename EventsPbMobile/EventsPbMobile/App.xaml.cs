using EventsPbMobile.Classes;
using EventsPbMobile.Pages;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace EventsPbMobile
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;
        private readonly EventsDataAccess _dataAccess;

        public App()
        {
            InitializeComponent();

            var page = new TabbedPage();
            page.On<Android>().SetIsSwipePagingEnabled(false);

            AdvancedSearchOptions.Date = true;
            AdvancedSearchOptions.Place = true;
            AdvancedSearchOptions.Title = true;
            AdvancedSearchOptions.Text = true;

            _dataAccess = new EventsDataAccess();
            InitNotificationSettings();
            CheckIfFirstLauch();
        }

        public static INotification Notification { get; private set; }
        public static IDownloadManager DownloadManager { get; private set; }

        private void InitNotificationSettings()
        {
        }

        private void CheckIfFirstLauch()
        {
            var settings = _dataAccess.GetSettings();
            if (settings.AhotherLauchOfApp)
            {
                MainPage = new MainMenu();
            }
            else
            {
                var dbinstance = _dataAccess.GetDbInstance();
                dbinstance.Write(() => { settings.AhotherLauchOfApp = true; });
                MainPage = new Help();
            }
        }

        public static void Init(INotification notification)
        {
            Notification = notification;
        }

        public static void InitDownload(IDownloadManager manager)
        {
            DownloadManager = manager;
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