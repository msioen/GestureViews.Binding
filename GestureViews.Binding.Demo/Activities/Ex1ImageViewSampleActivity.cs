using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Widget;
using GestureViews.Binding.Demo.Adapters;
using GestureViews.Binding.Demo.Utils;
using System;
using Android.Views;

namespace GestureViews.Binding.Demo.Activities
{
    [Activity(
        Label = "Images in ViewPager",
        Theme = "@style/AppTheme.Full.NoTitle.Dark")]
    [IntentFilter(
        new string[] { Intent.ActionMain },
        Categories = new string[] { Intent.CategorySampleCode })]
    public class Ex1ImageViewSampleActivity : BaseActivity, ViewPager.IOnPageChangeListener
    {
        Painting[] _paintings;

        ViewPager _viewPager;
        TextView _txtTitle;

        GestureSettingsMenu _settingsMenu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_ex1_image_view);
            Title = null;

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _paintings = PaintingsHelper.GetPaintings(Resources);

            _settingsMenu = new GestureSettingsMenu();

            _txtTitle = FindViewById<TextView>(Resource.Id.painting_title);

            _viewPager = FindViewById<ViewPager>(Resource.Id.paintings_view_pager);
            _viewPager.Adapter = new PaintingsPagerAdapter(_viewPager, _paintings, _settingsMenu);
            _viewPager.AddOnPageChangeListener(this);
            _viewPager.PageMargin = Resources.GetDimensionPixelSize(Resource.Dimension.view_pager_margin);
            OnPageSelected(0);
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

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageSelected(int position)
        {
            var title = _paintings[position].Author + System.Environment.NewLine + _paintings[position].Title;
            _txtTitle.Text = title;
        }
    }
}