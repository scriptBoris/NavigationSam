using CoreGraphics;
using Foundation;
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

        protected NavigationPageSam NavPage => Element as NavigationPageSam;
        protected bool IsIosOver14 => UIDevice.CurrentDevice.CheckSystemVersion(14, 0);

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InteractivePopGestureRecognizer.Delegate = CreateGestureBackDelegate();
        }

        // Software button back iOS 12
        [Export("navigationBar:didPushItem:")]
        public bool DidPushItem(UINavigationBar navigationBar, UINavigationItem item)
        {
            return OnDidPushItem(navigationBar, item);
        }

        // Software button back iOS 14
        public override UIViewController PopViewController(bool animated)
        {
            if (IsIosOver14)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
                });
                return null;
            }
            else
            {
                return base.PopViewController(animated);
            }
        }

        protected virtual bool OnDidPushItem(UINavigationBar navigationBar, UINavigationItem item)
        {
            if (IsIosOver14)
                return true;

            if (navigationBar.Items.Length > 1)
            {
                var view = navigationBar.Subviews[2].Subviews.FirstOrDefault();
                int count = view.GestureRecognizers?.Length ?? 0;
                if (count > 0)
                    for (int i = count; i > 0; i--)
                    {
                        var gesture = view.GestureRecognizers[i - 1];
                        view.RemoveGestureRecognizer(gesture);
                        gesture.Dispose();
                    }

                var tap = new UITapGestureRecognizer(async () =>
                {
                    await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
                });

                view.AddGestureRecognizer(tap);
            }

            return true;
        }

        protected virtual IUIGestureRecognizerDelegate CreateGestureBackDelegate()
        {
            var delegateBack = new SwipeBack(NavPage);
            return delegateBack;
        }
    }

    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
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
            if (!navPage.IsEnableGestureBackForIOS)
                return false;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await navPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
            });
            return false;
        }
    }
}
