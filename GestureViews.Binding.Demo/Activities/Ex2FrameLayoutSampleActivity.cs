using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using GestureViews.Binding.Demo.Adapters;
using GestureViews.Binding.Demo.Utils;

namespace GestureViews.Binding.Demo.Activities
{
    [Activity(
        Label = "Layouts in ViewPager",
        Theme = "@style/AppTheme.FrameLayout")]
    [IntentFilter(
        new string[] { Intent.ActionMain },
        Categories = new string[] { Intent.CategorySampleCode })]
    public class Ex2FrameLayoutSampleActivity : BaseActivity
    {
        ViewPager _viewPager;
        GestureSettingsMenu _settingsMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ex2_frame_layout);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _settingsMenu = new GestureSettingsMenu();

            var paintings = PaintingsHelper.GetPaintings(Resources);

            _viewPager = FindViewById<ViewPager>(Resource.Id.paintings_view_pager);
            _viewPager.Adapter = new PaintingsLayoutsPagerAdapter(_viewPager, paintings, _settingsMenu);
            _viewPager.PageMargin = Resources.GetDimensionPixelSize(Resource.Dimension.view_pager_margin);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return _settingsMenu.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (_settingsMenu.OnOptionsItemSelected(item))
            {
                SupportInvalidateOptionsMenu();
                _viewPager.Adapter.NotifyDataSetChanged();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}