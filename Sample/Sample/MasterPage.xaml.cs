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
        private NavigationPage nav1;
        private NavigationPage nav2;

        public MasterPage()
        {
            InitializeComponent();

            // Variant Code behind (for beginners)
            nav1 = new NavigationPageSam(new MainPage());

            // Variant MVVM (for pro)
            nav2 = new Variant2.ViewModels.MainVm().NavigationPage;

            // Disable swipe master menu for demonstration iOS (iPhone X)
            // allow to use system swipe back
            IsGestureEnabled = false;

            Detail = nav1;
        }

        private void OnVariantCodeBehind(object sender, EventArgs e)
        {
            Detail = nav1;
            IsPresented = !IsPresented;
        }

        private void OnVariantMvvm(object sender, EventArgs e)
        {
            Detail = nav2;
            IsPresented = !IsPresented;
        }
    }
}