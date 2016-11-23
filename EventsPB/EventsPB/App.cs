using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventsPB.View;
using Xamarin.Forms;

namespace EventsPB
{
    public class App : Application
    {

        public static MasterDetail MasterDetail;

        public App()
        {
            MasterDetail = new MasterDetail();
            MainPage = MasterDetail;
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
