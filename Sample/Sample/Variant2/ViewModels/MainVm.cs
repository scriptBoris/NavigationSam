using NavigationSam;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sample.Variant2.ViewModels
{
    public class MainVm : BaseViewModel, INavigationPopInterceptor
    {
        public MainVm()
        {
            CommandEdit = new Command(ActionEdit);
            CommandEditModal = new Command(ActionEditModal);

            App.CurrentUser.PropertyChanged += (o, e) => OnPropertyChanged(e.PropertyName);
        }

        public ICommand CommandEdit { get; set; }
        public ICommand CommandEditModal { get; set; }

        public string Name => App.CurrentUser.Name;
        public string Family => App.CurrentUser.Family;
        public DateTime DateBirth => App.CurrentUser.DateBirth;
        public string Email => App.CurrentUser.Email;
        public string AboutMe => App.CurrentUser.AboutMe;

        public override Page Page { get; set; } = new MainPage();

        public async Task<bool> RequestPop()
        {
            bool res = await Page.DisplayAlert("Question", "You are sure?", "Exit", "Cancel");
            return res;
        }

        private void ActionEdit()
        {
            GoTo(new EditVm(false));
        }

        private void ActionEditModal()
        {
            GoToModal(new EditVm(true));
        }
    }
}
