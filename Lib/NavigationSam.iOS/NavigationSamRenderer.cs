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
    public class NavigationSamRenderer : NavigationRenderer, IUIGestureRecognizerDelegate
    {
        public static void Preserve() { }

        public NavigationSamRenderer()
        {
        }

        private NavigationPageSam NavPage => Element as NavigationPageSam;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InteractivePopGestureRecognizer.Delegate = new SwipeBack(NavPage);
        }

        // Software button back
        public override UIViewController PopViewController(bool animated)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
            });
            return null;
        }
    }

    public class SwipeBack : UIGestureRecognizerDelegate
    {
        private readonly NavigationPageSam navPage;

        public static void Preserve() { }

        public SwipeBack(NavigationPageSam navPage)
        {
            this.navPage = navPage;
        }

        // Swipe back
        public override bool ShouldBegin(UIGestureRecognizer recognizer)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await navPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
            });
            return false;
        }
    }
}
