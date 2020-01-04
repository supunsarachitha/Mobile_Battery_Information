using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;

namespace Battery_Info
{
    [Service(Exported = true)]
    [IntentFilter(new String[] { "lk.stechbuzz.batteryinfo" })]
    public class BatteryService : Service
    {
        private Context _context;

        private bool _isRunning;
        BatteryBroadcastReceiver mReceiver;
        RestarterBroadcastReceiver mReceiver2;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnCreate()
        {
                base.OnCreate();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {          
            _context = this;
            base.OnStartCommand(intent, flags, startId);
            return StartCommandResult.Sticky;
        }



        public override void OnDestroy()
        {
            base.OnDestroy();
        }


        public override void OnTaskRemoved(Intent rootIntent)
        {
            
            Intent intent = new Intent("lk.stechbuzz.batteryinfo.RestartSensor");
            SendBroadcast(intent);

            base.OnTaskRemoved(rootIntent);
        }



    }
}