using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Widget;
using System;
using Xamarin.Essentials;

namespace Battery_Info
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        // Unique ID for our notification: 
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";
        internal static readonly string COUNT_KEY = "count";
        int count = 1;
        NotificationCompat.Builder builder;

        ISharedPreferences prefs = null;
        ToggleButton toggleNotification;


        TextView tLevel;
        TextView tStatus;
        TextView tHealth;
        TextView tTemperature;
        TextView tTechnology;
        TextView tVoltage;
        TextView tPowerSource;

        AndroidBattery battery;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;

            tLevel = FindViewById<TextView>(Resource.Id.Level);
            tStatus = FindViewById<TextView>(Resource.Id.Status);
            tHealth = FindViewById<TextView>(Resource.Id.Health);
            tTemperature = FindViewById<TextView>(Resource.Id.Temperature);
            tTechnology = FindViewById<TextView>(Resource.Id.Technology);
            tVoltage = FindViewById<TextView>(Resource.Id.voltage);
            tPowerSource = FindViewById<TextView>(Resource.Id.Source);

            CreateNotificationChannel();

            battery = new AndroidBattery();
            UpdateBatteryInfo();

            toggleNotification = FindViewById<ToggleButton>(Resource.Id.toggleNotification);
            toggleNotification.Checked = false;
            

            toggleNotification.CheckedChange += ToggleNotification_CheckedChange;

            getHistroy();



            //SetAlarmForBackgroundServices(this);

        }

        private void UpdateBatteryInfo()
        {
            battery = new AndroidBattery();
            tLevel.Text = String.Format("{0}%", battery.Level);
            tStatus.Text = String.Format("{0}", battery.Status);
            tHealth.Text = String.Format("{0}", battery.Health);
            tTemperature.Text = String.Format("{0} °C", battery.Temperature / 10);
            tTechnology.Text = String.Format("{0}", battery.Technology);
            tVoltage.Text = String.Format("{0:0.00} V", Convert.ToDecimal(battery.Voltage) / 1000);
            tPowerSource.Text = String.Format("{0}", Battery.PowerSource);
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            UpdateBatteryInfo();
        }

        private void ToggleNotification_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            
            if (toggleNotification.Checked)
            {
                prefs.Edit().PutBoolean("Notification", true).Commit();
                StartService(new Intent(this, typeof(BatteryService)));

            }
            else
            {
                NotificationManagerCompat.From(this).Cancel(NOTIFICATION_ID);
                prefs.Edit().PutBoolean("Notification", false).Commit();
                StopService(new Intent(this, typeof(BatteryService)));
            }

        }

        private void getHistroy()
        {

            bool vFirstRun = prefs.GetBoolean("Notification", false);
            if (vFirstRun)
            {
                toggleNotification.Checked = true;
            }
            else
            {
                toggleNotification.Checked = false;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        protected override void OnPause()
        {

            Battery.BatteryInfoChanged -= Battery_BatteryInfoChanged;
            base.OnPause();
        }


        protected override void OnDestroy()
        {
            Battery.BatteryInfoChanged -= Battery_BatteryInfoChanged;
            base.OnDestroy();
        }

        protected override void OnResume()
        {
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            UpdateBatteryInfo();
            base.OnResume();
        }

        protected override void OnStart()
        {
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            UpdateBatteryInfo();
            base.OnStart();
        }


        public static void SetAlarmForBackgroundServices(Context context)
        {
            BatteryBroadcastReceiver mReceiver = new BatteryBroadcastReceiver();
            context.RegisterReceiver(mReceiver, new IntentFilter(Intent.ActionBatteryChanged));


            var alarmIntent = new Intent(context.ApplicationContext, typeof(AlarmReceiver));
            var broadcast = PendingIntent.GetBroadcast(context.ApplicationContext, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);

            var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
            alarmManager.SetInexactRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime(), 30000, broadcast);
        }


        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }

            var name = this.Resources.GetString(Resource.String.channel_name);
            var description = this.GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };
            channel.EnableVibration(true);
            channel.SetVibrationPattern(new long[] { 0 });


            var notificationManager = (NotificationManager)this.GetSystemService("notification");
            notificationManager.CreateNotificationChannel(channel);
        }


    }
}