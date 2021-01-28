using NavigationSam;
using NavigationSam.iOS;
using NavigationSam.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPageSam), typeof(NavigationSamRenderer))]
namespace NavigationSam.iOS
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class NavigationSamRenderer : NavigationRenderer
    {
        public static void Preserve() { }

        public NavigationSamRenderer()
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        private NavigationPageSam NavPage => Element as NavigationPageSam;

        public override UIViewController PopViewController(bool animated)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
            });
            return null;
        }
    }
}
