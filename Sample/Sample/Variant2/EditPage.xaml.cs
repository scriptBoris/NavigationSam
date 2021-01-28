using Sample.Variant2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Variant2
{
    public partial class EditPage : ContentPage
    {
        public EditPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is BaseViewModel vm)
            {
                vm.GoBackByUser();
            }
            return true;
        }
    }
}