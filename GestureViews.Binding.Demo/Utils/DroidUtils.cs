using Android.OS;
using Android.Text;
using Android.Widget;

namespace GestureViews.Binding.Demo.Utils
{
    public static class DroidUtils
    {
        public static void SetHtmlText(this TextView tv, string html)
        {
            if (((int)Build.VERSION.SdkInt) >= 24)
            {
                tv.TextFormatted = Html.FromHtml(html, FromHtmlOptions.ModeLegacy);
            }
            else
            {
#pragma warning disable CS0618 // Type or member is obsolete
                tv.TextFormatted = Html.FromHtml(html);
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }
    }
}