using Android.Content;
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
//using AToolbar = AndroidX.AppCompat.Widget.Toolbar;

[assembly: ExportRenderer(typeof(NavigationPageSam), typeof(NavigationSamRenderer))]
namespace NavigationSam.Droid
{
    [Preserve(AllMembers = true)]
    public class NavigationSamRenderer : NavigationPageRenderer, IOnClickListener
    {
        private Android.Support.V7.Widget.Toolbar _toolbar;

        public static void Preserve() { }

        public NavigationSamRenderer(Context context) : base(context)
        {
            FormsAppCompatActivitySam.BackPressedSam += OnBackPressed;
        }

        private NavigationPageSam NavPage => Element as NavigationPageSam;


        #region override
        /// <summary>
        /// Click "Button arrow back" without master page
        /// </summary>
        async void IOnClickListener.OnClick(Android.Views.View v)
        {
            await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (_toolbar == null)
            {
                System.Collections.Generic.List<View> views = new System.Collections.Generic.List<View>();
                for (int i = 0; i < ChildCount; i++)
                {
                    var child = GetChildAt(i);
                    views.Add(child);

                    if (child is Android.Support.V7.Widget.Toolbar toolbar)
                    {
                        _toolbar = toolbar;
                        _toolbar.NavigationClick += OnNavigationClick;
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            FormsAppCompatActivitySam.BackPressedSam -= OnBackPressed;

            if (_toolbar != null)
                _toolbar.NavigationClick -= OnNavigationClick;
        }
        #endregion override


        #region methods
        private Task OnBackPressed(PopSources source, PopResult popResult)
        {
            return NavPage.CatchBackButton(source, popResult);
        }

        /// <summary>
        /// Click "Button arrow back" with master page
        /// </summary>
        private async void OnNavigationClick(object sender, Android.Support.V7.Widget.Toolbar.NavigationClickEventArgs e)
        {
            if (NavPage.Parent is MasterDetailPage master)
            {
                int navCount = NavPage.Navigation.NavigationStack.Count;
                int modCount = NavPage.Navigation.ModalStack.Count;

                if (navCount <= 1 && modCount == 0)
                {
                    master.IsPresented = !master.IsPresented;
                }
                else
                {
                    await NavPage.CatchBackButton(PopSources.SoftwareBackButton, new PopResult());
                }
            }
            else
            {
                NavPage?.PopAsync();
            }
        }
        #endregion methods
    }
}
