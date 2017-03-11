using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace EventsPbMobile.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SpashScreen : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartupMainPage();
        }

        private void StartupMainPage()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed() { }
    }

    
}