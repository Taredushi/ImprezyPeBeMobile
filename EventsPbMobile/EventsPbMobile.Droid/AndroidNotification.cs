using System;
using System.Threading;
using Android.App;
using Android.Content;
using EventsPbMobile.Classes;
using EventsPbMobile.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidNotification))]

namespace EventsPbMobile.Droid
{
    public class AndroidNotification : INotification
    {
        public void SetAlarm(string message, string title, int eventId, DateTimeOffset eventStartDate)
        {
            var alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            var pendingIntent = PendingIntent.GetBroadcast(Forms.Context, eventId, alarmIntent,
                PendingIntentFlags.UpdateCurrent);
            var alarmManager = (AlarmManager) Forms.Context.GetSystemService(Context.AlarmService);

            alarmManager.Set(AlarmType.RtcWakeup, eventStartDate.ToUnixTimeMilliseconds(), pendingIntent);
        }
        public void CancelAlarm(int eventId)
        {
            var intent = new Intent(Forms.Context, typeof(AlarmReceiver));
            var sender = PendingIntent.GetBroadcast(Forms.Context, eventId, intent, 0);
            var alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);
            alarmManager.Cancel(sender);
        }

        public void StartService()
        {
            var intent = new Intent(Forms.Context, typeof(NotificationService));
            var t = new Thread(() => { Forms.Context.StartService(intent); });
            t.Start();
        }

       
    }
}