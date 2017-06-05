using Android.Support.V4.View;
using Android.Views;
using Com.Alexvasilkov.Gestures.Commons;
using Com.Alexvasilkov.Gestures.Views;
using GestureViews.Binding.Demo.Utils;

namespace GestureViews.Binding.Demo.Adapters
{
    public class PaintingsPagerAdapter : RecyclePagerAdapter
    {
        ViewPager _viewPager;
        Painting[] _paintings;
        IGestureSettingsSetupListener _setupListener;

        public PaintingsPagerAdapter(ViewPager pager, Painting[] paintings,
            IGestureSettingsSetupListener setupListener)
        {
            _viewPager = pager;
            _paintings = paintings;
            _setupListener = setupListener;
        }

        public override int Count
        {
            get
            {
                return _paintings.Length;
            }
        }

        public override Java.Lang.Object OnCreateViewHolder(ViewGroup p0)
        {
            var holder = new PaintingsViewHolder(p0);
            holder.Image.Controller.EnableScrollInViewPager(_viewPager);
            return holder;
        }

        public override void OnBindViewHolder(Java.Lang.Object p0, int p1)
        {
            var holder = (PaintingsViewHolder)p0;
            _setupListener?.OnSetupGestureView(holder.Image);
            holder.Image.SetImageResource(_paintings[p1].ImageId);
        }

        class PaintingsViewHolder : RecyclePagerAdapter.ViewHolder
        {
            public GestureImageView Image { get; }

            public PaintingsViewHolder(ViewGroup container)
                : base(new GestureImageView(container.Context))
            {
                Image = (GestureImageView)ItemView;
            }
        }
    }
}