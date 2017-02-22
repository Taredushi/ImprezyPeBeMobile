using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;

namespace EventsPbMobile.Droid
{
    [Service]
    public class NotificationService : IntentService
    {
        private static readonly string TAG = "X:" + typeof(NotificationService).Name;
        private static readonly int TimerWait = 6000;
        private bool isStarted;
        private DateTime startTime;
        private Timer timer;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug(TAG, $"OnStartCommand called at {startTime}, flags={flags}, startid={startId}");
            if (isStarted)
            {
                var runtime = DateTime.UtcNow.Subtract(startTime);
                Log.Debug(TAG, $"This service was already started, it's been running for {runtime:c}.");
            }
            else
            {
                startTime = DateTime.UtcNow;
                Log.Debug(TAG, $"Starting the service, at {startTime}.");
                timer = new Timer(HandleTimerCallback, startTime, 0, TimerWait);
                isStarted = true;
            }
            return StartCommandResult.RedeliverIntent;
        }

        private void HandleTimerCallback(object state)
        {
            var runTime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"This service has been running for {runTime:c} (since ${state}).");
           // Device.BeginInvokeOnMainThread(
          //      () => { Toast.MakeText(Forms.Context, "Service dziala", ToastLength.Long).Show(); });
        }

        public override void OnDestroy()
        {
            timer.Dispose();
            timer = null;
            isStarted = false;

            var runtime = DateTime.UtcNow.Subtract(startTime);
            Log.Debug(TAG, $"Simple Service destroyed at {DateTime.UtcNow} after running for {runtime:c}.");
            Device.BeginInvokeOnMainThread(
                () => { Toast.MakeText(Forms.Context, "Service nie dziala", ToastLength.Long).Show(); });
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        protected override void OnHandleIntent(Intent intent)
        {
            
        }
    }
}