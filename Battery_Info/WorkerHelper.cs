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
using AndroidX.Work;

namespace Battery_Info
{
    class WorkerHelper : Worker
    {

        public WorkerHelper(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
        {

        }


        public override Result DoWork()
        {
            bool taxReturn = ShowMessage();
            //Android.Util.Log.Debug("CalculatorWorker", $"Your Tax Return is: {taxReturn}");
            return Result.InvokeRetry();
        }

        private bool ShowMessage()
        {


            return true;
        }

    }
}