using Android.Content;
using Android.Views;
using AndroidX.AppCompat.Widget;
using NavigationSam;
using NavigationSam.Droid;
using NavigationSam.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using static Android.Views.View;
using XfView = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(NavigationPageSam), typeof(NavigationSamRenderer))]
namespace NavigationSam.Droid
{
    [Preserve(AllMembers = true)]
    public class NavigationSamRenderer : NavigationPageRenderer, IOnClickListener
    {
        public static void Preserve() { }

        public NavigationSamRenderer(Context context) : base(context)
        {
            FormsAppCompatActivitySam.BackPressedSam += OnBackPressed;
            Device.Info.PropertyChanged += OnDevicePropertyChanged;
        }

        private NavigationPageSam NavPage => Element as NavigationPageSam;
        private IPageController PageController => Element;

        #region override
        /// <summary>
        /// Click "Button arrow back"
        /// </summary>
        async void IOnClickListener.OnClick(Android.Views.View v)
        {
            var mp = GetMasterPage();

            if (mp == null || Element.Navigation.NavigationStack.Count > 1)
            {
                await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
            }
            else
            {
                mp.IsPresented = !mp.IsPresented;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            UpdateToolbarInterceptFunctional();
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            UpdateToolbarInterceptFunctional();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            FormsAppCompatActivitySam.BackPressedSam -= OnBackPressed;
            Device.Info.PropertyChanged -= OnDevicePropertyChanged;
        }
        #endregion override


        #region methods
        private FlyoutPage GetMasterPage()
        {
            FlyoutPage master = null;
            Element page = Element.RealParent;
            while (page != null)
            {
                if (page is FlyoutPage masterPage)
                {
                    master = masterPage;
                    break;
                };

                page = page.RealParent;
            }

            if (master == null)
            {
                if (PageController.InternalChildren.Count > 0)
                    master = PageController.InternalChildren[0] as FlyoutPage;
            }

            return master;
        }

        private void UpdateToolbarInterceptFunctional()
        {
            var views = new System.Collections.Generic.List<XfView>();
            for (int i = 0; i < ChildCount; i++)
            {
                var child = GetChildAt(i);
                views.Add(child);

                if (child is Toolbar toolbar)
                    toolbar.SetNavigationOnClickListener(this);
            }
        }

        private Task OnBackPressed(PopSources source, PopResult popResult)
        {
            return NavPage.CatchBackButton(source, popResult);
        }

        private void OnDevicePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (nameof(Device.Info.CurrentOrientation) == e.PropertyName)
                UpdateToolbarInterceptFunctional();
        }
        #endregion methods
    }
}
