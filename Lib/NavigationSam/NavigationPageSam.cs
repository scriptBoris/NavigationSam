using NavigationSam.Utils;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace NavigationSam
{
    [Preserve(AllMembers = true)]
    public class NavigationPageSam : NavigationPage
    {
        public static void Preserve() { }

        public NavigationPageSam(Page root) : base(root)
        {
        }

        public bool IsRequestAppearing { get; private set; }

        public virtual async Task CatchBackButton(PopSources popSource, PopResult popResult)
        {
            var page = GetDisplayPage();
            if (page == null)
                return;

            // Get implement interface. Priority is Page
            INavigationPopInterceptor interceptor = null;
            if (page is INavigationPopInterceptor pageInterceptor && pageInterceptor != null)
            {
                interceptor = pageInterceptor;
            }
            else if (page.BindingContext is INavigationPopInterceptor vmInterceptor && vmInterceptor != null)
            {
                interceptor = vmInterceptor;
            }

            int count = GetNavigationCount();
            // For iOS, we are ignore back button intercept for root page
            if (count == 1 && Device.RuntimePlatform == Device.iOS)
            {
                return;
            }

            if (interceptor != null)
            {
                if (IsRequestAppearing)
                    return;

                IsRequestAppearing = true;
                bool res = await interceptor.RequestPop();
                IsRequestAppearing = false;

                if (res)
                {
                    if (popSource == PopSources.SoftwareBackButton)
                    {
                        await Pop(page);
                    }
                    else
                    {
                        if (count == 1)
                        {
                            popResult.IsContinueHardwareButton = true;
                        }
                        else
                        {
                            await Pop(page);
                        }
                    }
                }
            }
            else
            {
                if (popSource == PopSources.SoftwareBackButton)
                {
                    await Pop(page);
                }
            }
        }

        private async Task Pop(Page page)
        {
            if (IsModal(page))
                await page.Navigation.PopModalAsync();
            else
                await page.Navigation.PopAsync();
        }

        private Page GetDisplayPage()
        {
            int modalCount = Navigation.ModalStack.Count;
            int navCount = Navigation.NavigationStack.Count;

            if (modalCount == 0 && navCount == 0)
                return null;

            if (modalCount > 0)
            {
                return Navigation.ModalStack[modalCount - 1];
            }
            else
            {
                return Navigation.NavigationStack[navCount - 1];
            }
        }

        private int GetNavigationCount()
        {
            int modalCount = Navigation.ModalStack.Count;
            int navCount = Navigation.NavigationStack.Count;

            return modalCount + navCount;
        }

        private bool IsModal(Page page)
        {
            for (int i = 0; i < page.Navigation.ModalStack.Count; i++)
            {
                if (page == page.Navigation.ModalStack[i])
                    return true;
            }
            return false;
        }
    }
}
