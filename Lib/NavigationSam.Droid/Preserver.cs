using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationSam.Droid
{
    [Preserve(AllMembers = true)]
    public static class Preserver
    {
        public static void Preserve()
        {
            NavigationPageSam.Preserve();
            Utils.PopResult.Preserve();
            NavigationSamRenderer.Preserve();
            FormsAppCompatActivitySam.Preserve();
        }
    }
}