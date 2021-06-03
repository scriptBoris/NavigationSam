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
    public partial class SamplePage : ContentPage, INavigationPopInterceptor
    {
        private int count = 0;

        public SamplePage(string title = "Sample page", int count = 1)
        {
            InitializeComponent();
            this.count = count;

            if (string.IsNullOrEmpty(title))
                title = $"{count}";
            else
                title += $" {count}";

            Title = title;
            labelCount.Text = title;
            entryNextPageTitle.Text = "Sample page";
        }

        public bool IsPopRequest { get; set; }

        private void OnButtonNext(object sender, EventArgs e)
        {
            var page = new SamplePage(entryNextPageTitle.Text, count + 1);
            Navigation.PushAsync(page);
        }

        private void OnButtonBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void OnInsertBefore(object sender, EventArgs e)
        {
            var page = new SamplePage(entryNextPageTitle.Text, count + 1);
            Navigation.InsertPageBefore(page, this);
        }

        private void OnNavigationRoot(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void OnRemovePage(object sender, EventArgs e)
        {
            int count = Navigation.NavigationStack.Count;
            var prev = Navigation.NavigationStack[count - 2];
            Navigation.RemovePage(prev);
        }

        private void OnToolbarItemClicked(object sender, EventArgs e)
        {
            DisplayAlert("Success", "Toolbar item is worked!", "OK");
        }

        public Task<bool> RequestPop()
        {
            return Task.FromResult(true);
        }
    }
}