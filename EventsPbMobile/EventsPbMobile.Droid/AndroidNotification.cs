using System;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Runtime;
using EventsPbMobile.Classes;
using Java.Lang;
using Xamarin.Forms;
using Thread = System.Threading.Thread;

namespace EventsPbMobile.Droid
{
    public class AndroidNotification:INotification
    {
        public void ShowNotification(string title, string text)
        {
            Notification.Builder builder = new Notification.Builder(Forms.Context);
            builder.SetContentTitle(title);
            builder.SetContentText(text);
            builder.SetSmallIcon(Resource.Drawable.ic_media_play);

            Notification notification = builder.Build();

            NotificationManager notificationManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            const int notificationId = 0;

            notificationManager.Notify(notificationId,notification);

        }

        public void StartService()
        {            
            Intent intent = new Intent(Forms.Context,typeof(NotificationService));
            Thread t = new Thread(() => {
                Forms.Context.StartService(intent);
            });
            t.Start();
        }
    }
}