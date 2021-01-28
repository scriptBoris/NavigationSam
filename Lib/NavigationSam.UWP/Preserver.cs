using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationSam.UWP
{
    public static class Preserver
    {
        [Obsolete("Xamarin.Forms team didn't make it possible to intercept the back button. Use OnBackButtonPressed from Page")]
        public static void Preserve()
        {
            NavigationPageSam.Preserve();
            Utils.PopResult.Preserve();
            NavigationSamRenderer.Preserve();
        }
    }
}
