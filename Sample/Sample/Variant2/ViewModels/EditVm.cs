using NavigationSam;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sample.Variant2.ViewModels
{
    public class EditVm : BaseViewModel, INavigationPopInterceptor
    {
        public EditVm(bool isModal)
        {
            IsModal = isModal;
            CommandClose = new Command(ActionClose);
            CommandSave = new Command(ActionSave);
        }

        public bool IsModal { get; set; }
        public string Name { get; set; } = App.CurrentUser.Name;
        public string Family { get; set; } = App.CurrentUser.Family;
        public DateTime DateBirth { get; set; } = App.CurrentUser.DateBirth;
        public string Email { get; set; } = App.CurrentUser.Email;
        public string AboutMe { get; set; } = App.CurrentUser.AboutMe;

        public ICommand CommandClose { get; set; }
        public ICommand CommandSave { get; set; }

        public override Page Page { get; set; } = new EditPage();

        public async Task<bool> RequestPop()
        {
            bool res = true;

            if (CheckChanges())
                res = await Page.DisplayAlert("Warning",
                    "If you exit, all unsaved changes will be lost",
                    "Exit", "Cancel");

            return res;
        }

        private async void ActionClose()
        {
            if (await RequestPop())
                await GoBack();
        }

        private void ActionSave()
        {
            App.CurrentUser.Name = Name;
            App.CurrentUser.Family = Family;
            App.CurrentUser.DateBirth = DateBirth;
            App.CurrentUser.Email = Email;
            App.CurrentUser.AboutMe = AboutMe;
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
    }
}
