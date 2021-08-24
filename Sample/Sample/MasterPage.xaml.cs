using NavigationSam;
using Sample.Variant1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    public partial class MasterPage : MasterDetailPage
    {
        private NavigationPage navigation;

        public MasterPage()
        {
            InitializeComponent();

            // Variant Code behind (for beginners)
            navigation = new NavigationPageSam(new MainPage());

            // Disable swipe master menu for demonstration iOS (iPhone X)
            // allow to use system swipe back
            if (Device.RuntimePlatform == Device.iOS)
                IsGestureEnabled = false;

            Detail = navigation;
        }

        private async void OnButtonPopRoot(object sender, EventArgs e)
        {
            IsPresented = !IsPresented;

            // Do nothing if the user is already in the root page
            // Since the root page listen BackButton, to display the program exit dialog
            if (navigation.CurrentPage is MainPage)
                return;

            if (navigation.CurrentPage is INavigationPopInterceptor nav)
            {
                // If page dont permission back then do nothing
                if (!await nav.RequestPop())
                    return;
            }

            await navigation.PopToRootAsync();
        }
    }
}