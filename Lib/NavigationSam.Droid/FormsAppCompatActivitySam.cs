using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NavigationSam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

namespace NavigationSam.Droid
{
    [Preserve(AllMembers = true)]
    public class FormsAppCompatActivitySam : FormsAppCompatActivity, IDisposable
    {
        internal static void Preserve() { }

        public static event BackButtonPressedSamEventHandler BackPressedSam;
        public delegate Task BackButtonPressedSamEventHandler(PopSources popSource, PopResult popResult);

        public FormsAppCompatActivitySam() : base()
        {
        }


        public async override void OnBackPressed()
        {
            if (BackPressedSam == null)
            {
                base.OnBackPressed();
            }
            else  
            {
                var popResult = new PopResult();
                await BackPressedSam(PopSources.HardwareBackButton, popResult);

                // When is root page
                if (popResult.IsContinueHardwareButton)
                    base.OnBackPressed();
            }
        }
    }
}