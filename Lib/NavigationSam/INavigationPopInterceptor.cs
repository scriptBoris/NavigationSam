using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace NavigationSam
{
    [Preserve(AllMembers = true)]
    public interface INavigationPopInterceptor
    {
        Task<bool> RequestPop();
    }
}
