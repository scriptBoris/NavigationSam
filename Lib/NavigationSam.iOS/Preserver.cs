using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace NavigationSam.iOS
{
    public static class Preserver
    {
        public static void Preserve()
        {
            NavigationPageSam.Preserve();
            Utils.PopResult.Preserve();
            NavigationSamRenderer.Preserve();
        }
    }
}