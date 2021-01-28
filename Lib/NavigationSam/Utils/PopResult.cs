using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace NavigationSam.Utils
{
    [Preserve(AllMembers = true)]
    public class PopResult
    {
        public static void Preserve() { }

        public bool IsContinueHardwareButton { get; set; }
    }
}
