using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Battery_Info
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { "lk.stechbuzz.batteryinfo.RestartSensor" })]
    public class RestarterBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var chk = new ChecRunning();

            if (!chk.isMyServiceRunning(typeof(BatteryService)))
            {
                Task.Delay(100);
                context.StartService(new Intent(context, typeof(BatteryService)));
                Toast.MakeText(context, "RestartSensor!", ToastLength.Short).Show();
            }
            
        }
    }
}