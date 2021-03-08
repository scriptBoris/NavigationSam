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
        /// <summary>
        /// Servise property for iOS
        /// </summary>
        bool IsPopRequest { get; set; }

        Task<bool> RequestPop();
    }
}
