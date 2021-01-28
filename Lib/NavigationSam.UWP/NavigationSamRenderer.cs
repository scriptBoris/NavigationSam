using NavigationSam;
using NavigationSam.UWP;
using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NavigationPageSam), typeof(NavigationSamRenderer))]
namespace NavigationSam.UWP
{
    [Preserve(AllMembers = true)]
    public class NavigationSamRenderer : NavigationPageRenderer
    {
        [Obsolete("Xamarin.Forms team didn't make it possible to intercept the back button. Use OnBackButtonPressed from Page")]
        public static void Preserve() { }
    }
}
