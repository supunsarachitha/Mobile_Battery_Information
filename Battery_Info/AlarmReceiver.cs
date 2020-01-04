using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler = Android.Support.V4.App.TaskStackBuilder;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Java.Lang;
using Android.Widget;

namespace Battery_Info
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {

        bool initiate=false;
        public override void OnReceive(Context context, Intent intent)
        {
            var resultIntent = new Intent(context, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var stackBuilder = TaskScheduler.Create(context);
            stackBuilder.AddParentStack(Class.FromType(typeof(MainActivity)));
            stackBuilder.AddNextIntent(resultIntent);

            var chk = new ChecRunning();

            if (!chk.isMyServiceRunning(typeof(BatteryService)))
            {
                Toast.MakeText(context, "Service Restarted!", ToastLength.Long).Show();
                var backgroundServiceIntent = new Intent(context, typeof(BatteryService));
                context.StartForegroundService(backgroundServiceIntent);
            }
            else
            {
                Toast.MakeText(context, "alarm tick!", ToastLength.Long).Show();
            }


            //if (!initiate)
            //{
            //    BatteryBroadcastReceiver mReceiver = new BatteryBroadcastReceiver();
            //    context.RegisterReceiver(mReceiver, new IntentFilter(Intent.ActionBatteryChanged));
            //}
            


        }
    }
}