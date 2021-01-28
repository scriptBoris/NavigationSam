using NavigationSam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Variant1
{
    public partial class MainPage : ContentPage, INavigationPopInterceptor
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            App.CurrentUser.PropertyChanged += (o, e) => OnPropertyChanged(e.PropertyName);
        }

        public string Name => App.CurrentUser.Name;
        public string Family => App.CurrentUser.Family;
        public DateTime DateBirth => App.CurrentUser.DateBirth;
        public string Email => App.CurrentUser.Email;
        public string AboutMe => App.CurrentUser.AboutMe;

        public async Task<bool> RequestPop()
        {
            bool res = await DisplayAlert("Question", "You are sure?", "Exit", "Cancel");
            return res;
        }

        private void OpenEdit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage(false));
        }

        private void OpenEditModal(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EditPage(true));
        }
    }
}
