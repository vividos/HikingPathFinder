using Android.OS;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

// export renderer so that it's used in the app
[assembly: ExportRenderer(typeof(MasterDetailPage), typeof(HikingPathFinder.App.Android.AndroidMasterDetailRenderer))]

namespace HikingPathFinder.App.Android
{
    /// <summary>
    /// From https://forums.xamarin.com/discussion/60315/appcompat-masterdetailpage-with-masterbehavior-split
    /// </summary>
    public class AndroidMasterDetailRenderer : MasterDetailPageRenderer
    {
        /// <summary>
        /// Called when a visual element has changed
        /// </summary>
        /// <param name="oldElement">old element</param>
        /// <param name="newElement">new element</param>
        protected override void OnElementChanged(VisualElement oldElement, VisualElement newElement)
        {
            base.OnElementChanged(oldElement, newElement);

            if (newElement == null)
            {
                return;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var drawer = GetChildAt(1);
                var detail = GetChildAt(0);

                var padding = detail.GetType().GetRuntimeProperty("TopPadding");
                int value = (int)padding.GetValue(detail);

                padding.SetValue(drawer, value);
            }
        }
    }
}
