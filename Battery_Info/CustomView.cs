using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Battery_Info
{
    public class RemoteViews : View
    {
        public RemoteViews(Context context) :base(context)
        {

        }
        public RemoteViews(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {

        }

        public RemoteViews(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {

        }
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {

        }
    }
}