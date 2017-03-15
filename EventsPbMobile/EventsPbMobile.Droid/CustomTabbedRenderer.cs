using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.App;
using System;
using System.Reflection;
using Android.Graphics;
using Android.Widget;
using EventsPbMobile.Droid;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedRenderer))]

namespace EventsPbMobile.Droid
{
    public class CustomTabbedRenderer : TabbedPageRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            var info = typeof(TabbedPageRenderer).GetTypeInfo();
            // Disable animations only when UseAnimations is queried for enabling gestures
            var fieldInfo = info.GetField("_useAnimations", BindingFlags.Instance | BindingFlags.NonPublic);

            fieldInfo.SetValue(this, false);

            base.OnElementChanged(e);

            // Re-enable animations for everything else
            fieldInfo.SetValue(this, true);
        }
    }
}