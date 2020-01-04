using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Battery_Info
{
    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new string[] { Intent.ActionBootCompleted, Intent.ActionLockedBootCompleted, "android.intent.action.QUICKBOOT_POWERON", "com.htc.intent.action.QUICKBOOT_POWERON" })]
    public class BootReceiver : BroadcastReceiver
    {

        ISharedPreferences prefs = null;
        public override void OnReceive(Context context, Intent intent)
        {

            

            if (intent.Action.Equals(Intent.ActionBootCompleted))
            {
                prefs = PreferenceManager.GetDefaultSharedPreferences(context);
                if (prefs.GetBoolean("Notification", false))
                {
                    var serviceIntent = new Intent(context, typeof(BatteryService));
                    serviceIntent.AddFlags(ActivityFlags.NewTask);
                    context.StartForegroundService(serviceIntent);
                }
                
                
            }
        }
    }
}