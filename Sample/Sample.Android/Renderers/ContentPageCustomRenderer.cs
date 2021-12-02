using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using Sample.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageCustomRenderer))]
namespace Sample.Droid.Renderers
{
    public class ContentPageCustomRenderer : PageRenderer
    {
        public ContentPageCustomRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            Activity context = (Activity)this.Context;
            // to find out the back arrow
            var toolbar = context.FindViewById<Toolbar>(Resource.Id.toolbar);

            if (toolbar != null)
                //to set which string to announce.
                toolbar.NavigationContentDescription = "Go to previous page";
        }
    }
}