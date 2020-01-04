using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Battery_Info.Model;
using System;

namespace Battery_Info
{
    internal class AndroidBattery
    {

        private Battery battery;

        public AndroidBattery()
        {
            this.battery = new Battery();
            Update();
        }

        public int Level => this.battery.Level;
        public int Scale => this.battery.Scale;
        public BatteryStatus Status => this.battery.Status;
        public BatteryHealth Health => this.battery.Health;
        public float Temperature => this.battery.Temperature;
        public string Technology => this.battery.Technology;

        public int Voltage => this.battery.Voltage;

        private void Update()
        {
            try
            {
                var ifilter = new IntentFilter(Intent.ActionBatteryChanged);
                Intent batteryStatusIntent = Application.Context.RegisterReceiver(null, ifilter);
                this.battery = GetBatteryValuesFromIntent(batteryStatusIntent);
            }
            catch (Exception e)
            {
                Log.Debug("", e.Message);
            }
        }

        private Battery GetBatteryValuesFromIntent(Intent intent)
        {
            int statusExtra = intent.GetIntExtra(BatteryManager.ExtraStatus, -1);
            BatteryStatus status = this.GetBatteryStatus(statusExtra);
            int healthExtra = intent.GetIntExtra(BatteryManager.ExtraHealth, -1);
            BatteryHealth health = this.GetBatteryHealth(healthExtra);
            float tempExtra = intent.GetIntExtra(BatteryManager.ExtraTemperature, -1);
            string techExtra = intent.GetStringExtra(BatteryManager.ExtraTechnology);
            int voltage = intent.GetIntExtra(BatteryManager.ExtraVoltage, -1);

            return new Battery
            {
                Level = intent.GetIntExtra(BatteryManager.ExtraLevel, 0),
                Scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1),
                Status = status,
                Health = health,
                Temperature = tempExtra,
                Technology = techExtra,
                Voltage=voltage
                
            };
        }

        private BatteryStatus GetBatteryStatus(int status)
        {
            var result = BatteryStatus.Unknown;
            if (Enum.IsDefined(typeof(BatteryStatus), status))
            {
                result = (BatteryStatus)status;
            }
            return result;
        }


        private BatteryHealth GetBatteryHealth(int health)
        {
            var result = BatteryHealth.Unknown;
            if (Enum.IsDefined(typeof(BatteryHealth), health))
            {
                result = (BatteryHealth)health;
            }
            return result;
        }

    }
}