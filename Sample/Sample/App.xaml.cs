using NavigationSam;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var master = new MasterPage();

            if (Device.RuntimePlatform == Device.UWP)
                master.MasterBehavior = MasterBehavior.Popover;

            MainPage = master;
        }

        public static User CurrentUser { get; set; } = new User();

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
