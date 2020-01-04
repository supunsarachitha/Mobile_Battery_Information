using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Battery_Info.Model
{
    public class Battery
    {
        public int Level { get; set; }
        public int Scale { get; set; }
        public BatteryStatus Status { get; set; }
        public BatteryHealth Health { get; set; }
        public float Temperature { get; set; }
        public string Technology { get; set; }

        public int Voltage { get; set; }
    }
}