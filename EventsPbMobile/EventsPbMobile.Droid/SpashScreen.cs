using Android.App;
using Android.Content;
using Android.Support.V7.App;

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

        public override void OnBackPressed()
        {
        }
    }
}