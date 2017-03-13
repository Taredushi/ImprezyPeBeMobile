using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Debug = System.Diagnostics.Debug;

namespace EventsPbMobile.Droid
{
    [Activity(Label = "Eventy na Politechnice", Icon = "@drawable/icon", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            FormsMaps.Init(this, bundle);

            App.Init(new AndroidNotification());
            LoadApplication(new App());
        }

        protected override void OnDestroy()
        {
            try
            {
                base.OnDestroy();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}