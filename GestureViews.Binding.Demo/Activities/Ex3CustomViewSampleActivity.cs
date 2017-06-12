using Android.App;
using Android.Content;
using Android.OS;
using GestureViews.Binding.Demo.Views;

namespace GestureViews.Binding.Demo.Activities
{
    [Activity(
        Label = "Custom view",
        Theme = "@style/AppTheme")]
    [IntentFilter(
        new string[] { Intent.ActionMain },
        Categories = new string[] { Intent.CategorySampleCode })]
    public class Ex3CustomViewSampleActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ex3_custom_view);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            var textView = FindViewById<GestureTextView>(Resource.Id.text_view);
            textView.Controller.Settings.SetMaxZoom(1.5f);
        }
    }
}