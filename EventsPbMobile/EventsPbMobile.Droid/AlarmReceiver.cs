using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Java.Lang;

namespace EventsPbMobile.Droid
{
    [BroadcastReceiver(Enabled = true, Process = ":remote")]
    [IntentFilter(new[] {"android.intent.action.BOOT_COMPLETED"}, Priority = (int) IntentFilterPriority.LowPriority)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);
            var manager = NotificationManagerCompat.From(context);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(context)
                .SetContentIntent(contentIntent)
                .SetSmallIcon(Resource.Drawable.logo)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetStyle(style)
                .SetWhen(JavaSystem.CurrentTimeMillis())
                .SetPriority((int) NotificationPriority.High)
                .SetDefaults((int) NotificationDefaults.All)
                .SetAutoCancel(true);

            var notification = builder.Build();
            manager.Notify(0, notification);
        }
    }
}