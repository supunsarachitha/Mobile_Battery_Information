using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace Battery_Info
{
    [BroadcastReceiver]
    public class BatteryBroadcastReceiver : BroadcastReceiver
    {
        private Context _context;
        // Unique ID for our notification: 
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";
        int count = 1;
        NotificationCompat.Builder builder;
        ISharedPreferences prefs = null;

        public override void OnReceive(Context context, Intent intent)
        {
            _context = context;

            

            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            if(prefs.GetBoolean("Notification", false))
            {

                startNotification();
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        private void startNotification()
        {
            // Pass the current button press count value to the next activity:
            var valuesForActivity = new Bundle();
            valuesForActivity.PutInt(COUNT_KEY, count);

            // When the user clicks the notification, SecondActivity will start up.
            var resultIntent = new Intent(_context, typeof(MainActivity));

            // Pass some values to SecondActivity:
            resultIntent.PutExtras(valuesForActivity);

            // Construct a back stack for cross-task navigation:
            var stackBuilder = Android.App.TaskStackBuilder.Create(_context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            var resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            AndroidBattery battery = new AndroidBattery();
            RemoteViews R = new RemoteViews(_context.PackageName, Resource.Layout.remoteView);

            var tLevel= String.Format("{0}%", battery.Level);
            var tStatus = String.Format("{0}", battery.Status);
            

            if (Battery.State == BatteryState.Charging)
            {
                R.SetImageViewResource(Resource.Id.NotificationIcon, Resource.Drawable.volt);
                R.SetTextViewText(Resource.Id.notification_title, (tLevel + " " + tStatus + " on " + Battery.PowerSource.ToString()));
            }
            else
            {
                R.SetTextViewText(Resource.Id.notification_title, (tLevel + "  " + tStatus));
                R.SetImageViewResource(Resource.Id.NotificationIcon, Resource.Drawable.NotificationIcon);
            }
            

            // Build the notification:
            builder = new NotificationCompat.Builder(_context, CHANNEL_ID)
                          .SetAutoCancel(false)
                          .SetOngoing(true)// Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetSmallIcon(Resource.Drawable.SmallIcon) // This is the icon to display
                          .SetCustomContentView(R)
                          .SetShowWhen(false)
                          .SetStyle(new NotificationCompat.DecoratedCustomViewStyle());

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(_context);
            
            notificationManager.Notify(NOTIFICATION_ID, builder.Build());


        }



    }
}