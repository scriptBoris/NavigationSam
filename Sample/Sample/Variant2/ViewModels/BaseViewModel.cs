using NavigationSam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample.Variant2.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static NavigationPage navPage;

        public BaseViewModel()
        {
            Page.BindingContext = this;

            if (NavigationPage == null)
                NavigationPage = new NavigationPageSam(Page);
        }

        public NavigationPage NavigationPage { get => navPage; private set => navPage = value; }
        public abstract Page Page { get; set; }

        public Task GoTo(BaseViewModel vm)
        {
            return NavigationPage.Navigation.PushAsync(vm.Page);
        }

        public Task GoToModal(BaseViewModel vm)
        {
            return NavigationPage.Navigation.PushModalAsync(vm.Page);
        }

        public Task GoBack()
        {
            if (NavigationPage.Navigation.ModalStack.Count > 0)
                return NavigationPage.Navigation.PopModalAsync();
            else
                return NavigationPage.Navigation.PopAsync();
        }

        public async void GoBackByUser()
        {
            if (this is INavigationPopInterceptor interceptor)
            {
                if (await interceptor.RequestPop())
                    await GoBack();
            }
            else
            {
                await GoBack();
            }
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
