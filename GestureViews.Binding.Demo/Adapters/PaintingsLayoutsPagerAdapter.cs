using Android.App;
using Android.Content;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Alexvasilkov.Gestures.Commons;
using Com.Alexvasilkov.Gestures.Views;
using GestureViews.Binding.Demo.Utils;

namespace GestureViews.Binding.Demo.Adapters
{
    public class PaintingsLayoutsPagerAdapter : RecyclePagerAdapter, View.IOnClickListener
    {
        ViewPager _viewPager;
        Painting[] _paintings;

        public PaintingsLayoutsPagerAdapter(ViewPager pager, Painting[] paintings)
        {
            _viewPager = pager;
            _paintings = paintings;
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
            var holder = new PaintingsLayoutViewHolder(LayoutInflater.FromContext(p0.Context).Inflate(Resource.Layout.item_painting_layout, p0, false));
            holder.Layout.Controller.Settings.SetMaxZoom(3f);
            holder.Layout.Controller.EnableScrollInViewPager(_viewPager);
            return holder;
        }

        public override void OnBindViewHolder(Java.Lang.Object p0, int p1)
        {
            var holder = (PaintingsLayoutViewHolder)p0;
            holder.Layout.Controller.ResetState();

            holder.Image.SetImageResource(_paintings[p1].ImageId);

            var html = "<b>" + _paintings[p1].Author + "</b><br/>" + _paintings[p1].Title;
            DroidUtils.SetHtmlText(holder.Title, html);

            holder.Button.Tag = _paintings[p1].Link;
            holder.Button.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            var url = (string)v.Tag;
            Intent i = new Intent(Intent.ActionView);
            i.SetData(Android.Net.Uri.Parse(url));
            ((Activity)(v.Context)).StartActivity(i);
        }

        class PaintingsLayoutViewHolder : RecyclePagerAdapter.ViewHolder
        {

            public GestureFrameLayout Layout { get; }
            public ImageView Image { get; }
            public TextView Title { get; }
            public View Button { get; }

            public PaintingsLayoutViewHolder(View itemView)
                : base(itemView)
            {
                Layout = itemView.FindViewById<GestureFrameLayout>(Resource.Id.painting_g_layout);
                Image = itemView.FindViewById<ImageView>(Resource.Id.painting_image);
                Title = itemView.FindViewById<TextView>(Resource.Id.painting_title);
                Button = itemView.FindViewById<View>(Resource.Id.painting_button);
            }
        }
    }
}