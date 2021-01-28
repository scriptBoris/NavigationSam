using NavigationSam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Variant1
{
    public partial class EditPage : ContentPage, INavigationPopInterceptor
    {
        [Obsolete("Use secondary ctor")]
        public EditPage()
        {
            InitializeComponent();
        }

        public EditPage(bool isModal)
        {
            InitializeComponent();
            BindingContext = this;
            IsModal = isModal;
        }

        public bool IsModal { get; set; }
        public string Name { get; set; } = App.CurrentUser.Name;
        public string Family { get; set; } = App.CurrentUser.Family;
        public DateTime DateBirth { get; set; } = App.CurrentUser.DateBirth;
        public string Email { get; set; } = App.CurrentUser.Email;
        public string AboutMe { get; set; } = App.CurrentUser.AboutMe;


        public async Task<bool> RequestPop()
        {
            bool res = true;

            if (CheckChanges())
                res = await DisplayAlert("Warning",
                    "If you exit, all unsaved changes will be lost", 
                    "Exit", "Cancel");

            return res;
        }

        // For UWP
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool res = true;

                if (CheckChanges())
                    res = await RequestPop();

                if (res)
                    await Navigation.PopAsync();
            });
            return true;
        }

        private bool CheckChanges()
        {
            if (Name != App.CurrentUser.Name)
                return true;

            if (Family != App.CurrentUser.Family)
                return true;

            if (DateBirth != App.CurrentUser.DateBirth)
                return true;

            if (Email != App.CurrentUser.Email)
                return true;

            if (AboutMe != App.CurrentUser.AboutMe)
                return true;

            return false;
        }

        private void SaveChanges()
        {
            App.CurrentUser.Name = Name;
            App.CurrentUser.Family = Family;
            App.CurrentUser.DateBirth = DateBirth;
            App.CurrentUser.Email = Email;
            App.CurrentUser.AboutMe = AboutMe;
        }

        private async void OnClickedClose(object sender, EventArgs e)
        {
            if (await RequestPop())
                await Navigation.PopModalAsync();
        }

        private void OnClickedSave(object sender, EventArgs e)
        {
            SaveChanges();
        }
    }
}