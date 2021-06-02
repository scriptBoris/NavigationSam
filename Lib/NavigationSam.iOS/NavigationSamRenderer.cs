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
        protected string IconBase64_1x => "iVBORw0KGgoAAAANSUhEUgAAAA0AAAAVCAYAAACdbmSKAAAAAXNSR0IArs4c6QAAAKhJREFUOBG1lEEOgkAMRXsQTIoXYYMey6vhQVxUD4FLfcU0mZhMB6I0ebKY/1oCRZF1dSQ2wWFdXMSFB7zgBh2k1XMagkszDJnhwh08HMK4q/CkezpBCRjELblwgmopJwY/CWcaVEs5MSgnpAJZuRaCixdolpIw2DSJvCgY/EVMHzdDllJ+y9VpvqePJvK9c82NqIm+3ekq7SIO0TW7bv5yo5mLEyz/EW+12k/M/Q3EegAAAABJRU5ErkJggg==";
        protected string IconBase64_2x => "iVBORw0KGgoAAAANSUhEUgAAABoAAAAqCAYAAACtMEtjAAAAAXNSR0IArs4c6QAAAQtJREFUWAndmFsOAiEMRY3+uQRfe3Bt6v/41o25B59b0UvCzRBirKalk5GEUIbQw22YoUyvY1+OcDlFPdm7rj0eYD5jndePba0UUgz2DkLYzErPPoaKjvP2jPG+FiZBLgAMtJAdHOSrT/tukKFWyVZQcsV4OyAbDyUS5IZFjFBVZY3Z6W7K7f+B3KF0rIoVJq+EcLUHsvRQIkEeWMQEVVUqzM63bdpvD6QblajC8cvkKgLTcKW2Sei4IJfN8C3M5EUlzOWr0AjM5XigMleYy+lKZRIsZD7qo5wwlzSrEZiUCpskkFQmwUzy7kZgLtcWKpNgJhcxwlyulp9gxW7m4R8DT+ZiECoLsAU7bF/6qTg/zOn3HwAAAABJRU5ErkJggg==";
        protected string IconBase64_3x => "iVBORw0KGgoAAAANSUhEUgAAACcAAAA/CAYAAABjJtHDAAAAAXNSR0IArs4c6QAAAcxJREFUaAXtmstOwzAQRbvgsWMJLMOr/VX4Fd7PbfkKYBsK7ZJfgDsRV6pQFI1d+dpYHelmNo7m+MZyEtujUb6YoPQU2s+H0F/ZwBbQN/QGFQO4DGZwpldoD8oaY1SnYwRjzgo4BJYV0MDmECGG8gva7UKS8Dj2F/ZUQXaCIl7HCHiOezZSw1UFdqFy7BOF+Jg8WQJ2DKhQsEuFY1WBXakc+wgcYxKwI0CFgl3jnk0oaVQFdgOrinRMAnaI3s8gz8TKNrcKx6oCuyvVMQnYAXr/HjjG7tF+C0oaa7BQe2Mce0CR5I+yQZEW4hzlycWCPaIjRTpWLNgTHNuGksczKnjGFtt8of1OcqrfAg1yC7G4J5tzyccaanTR4NpCHjC2kYw5MHXR4NpCLO7JUsBiJ2CY1kWVgJKvkVUclALGfJrbh6ZsmokFTP4byEccAyj56/o3gLYEMYM8EzTbSH6o6WDMGskakO5ZjnFQsvxFyOIBY9aCJSubdLBKQMky/yoOSgFtzyt0o0Syg0MHDXAO8S3hycUDSrYz6eA4wsHiAc/YO0U2BxeQZ+xJ9/jZeQ9g1uMbkwEHs4LRwT5AOzKU/UROH2BRZ5mWAQdPgf0A+ffOGzJMYP0AAAAASUVORK5CYII=";

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InteractivePopGestureRecognizer.Delegate = CreateGestureBackDelegate();

            if (!IsIosOver14)
            {
                NavPage.InsertPageBeforeRequested += OnInsertPageBeforeRequested;
                NavPage.RemovePageRequested += OnRemovePageRequested;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsIosOver14)
            {
                NavPage.InsertPageBeforeRequested -= OnInsertPageBeforeRequested;
                NavPage.RemovePageRequested -= OnRemovePageRequested;
            }

            base.Dispose(disposing);
        }

        private void OnInsertPageBeforeRequested(object sender, NavigationRequestedEventArgs e)
        {
            UpdateAllPages();
        }

        private void OnRemovePageRequested(object sender, NavigationRequestedEventArgs e)
        {
            UpdateAllPages();
        }

        // Software button back iOS 12
        [Export("navigationBar:shouldPushItem:")]
        public bool ShouldPushItem(UINavigationBar navigationBar, UINavigationItem item)
        {
            if (IsIosOver14)
            {
                return true;
            }

            if (navigationBar.Items.Length > 0)
            {
                var page = NavPage.Navigation.NavigationStack.LastOrDefault();

                var backButton = CreateBackButtonForIOS12(navigationBar.Items.LastOrDefault(), page);
                if (backButton != null)
                {
                    item.HidesBackButton = true;
                    item.LeftBarButtonItem = backButton;
                }
            }

            return true;
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

        public virtual UIBarButtonItem CreateBackButtonForIOS12(UINavigationItem previousItem, Page xamarinFormsPage)
        {
            if (previousItem == null)
                return null;

            if (!NavigationPage.GetHasBackButton(xamarinFormsPage))
                return null;

            var data = new NSData(IconBase64_3x, NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
            var image = UIImage.LoadFromData(data);
            image = image.Scale(new CGSize(13, 21), 0.0f);


            string backText = previousItem.Title;
            if (string.IsNullOrWhiteSpace(backText))
                backText = "Back";

            string backTextTemp = NavigationPage.GetBackButtonTitle(xamarinFormsPage);
            if (backTextTemp != null)
                backText = backTextTemp;


            // init fake backButton:
            var uibutton = new UIButton(UIButtonType.System);
            uibutton.AddTarget(
                async (o, e) =>
                {
                    await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
                }, UIControlEvent.TouchUpInside);
            uibutton.Frame = new CGRect(0, 0, 53, 32);
            uibutton.SemanticContentAttribute = UISemanticContentAttribute.ForceLeftToRight;
            uibutton.SetTitle(backText, UIControlState.Normal);
            uibutton.TitleEdgeInsets = new UIEdgeInsets(0, -3, 0, 3);
            uibutton.TitleLabel.TextAlignment = UITextAlignment.Right;

            uibutton.SetImage(image, UIControlState.Normal);
            uibutton.ImageView.AutoresizingMask = UIViewAutoresizing.None;
            uibutton.ImageView.ContentMode = UIViewContentMode.Left;
            uibutton.ImageEdgeInsets = new UIEdgeInsets(0, -7, 0, 7);

            var barButtonCustom = new UIBarButtonItem(uibutton);
            return barButtonCustom;
        }

        protected virtual IUIGestureRecognizerDelegate CreateGestureBackDelegate()
        {
            var delegateBack = new SwipeBack(NavPage);
            return delegateBack;
        }

        protected virtual void UpdateAllPages()
        {
            if (NavigationBar.Items != null)
            {
                UINavigationItem previous = null;

                int index = 0;
                var stack = NavPage.Navigation.NavigationStack;
                foreach (var item in NavigationBar.Items)
                {
                    if (index > 0)
                    {
                        if (item.LeftBarButtonItem?.CustomView is UIButton)
                            item.LeftBarButtonItem.CustomView.Dispose();

                        item.LeftBarButtonItem = CreateBackButtonForIOS12(previous, stack[index]);
                    }

                    previous = item;
                    index++;
                }
            }
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
