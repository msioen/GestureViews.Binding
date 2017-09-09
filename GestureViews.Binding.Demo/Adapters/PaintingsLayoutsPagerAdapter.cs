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
    public class PaintingsLayoutsPagerAdapter : RecyclePagerAdapter<PaintingsLayoutViewHolder>, View.IOnClickListener
    {
        const float MAX_ZOOM = 1.5f;

        ViewPager _viewPager;
        Painting[] _paintings;
        IGestureSettingsSetupListener _setupListener;

        public PaintingsLayoutsPagerAdapter(ViewPager pager, Painting[] paintings,
            IGestureSettingsSetupListener setupListener)
        {
            _viewPager = pager;
            _paintings = paintings;
            _setupListener = setupListener;
        }

        public override int Count
        {
            get { return _paintings.Length; }
        }

        public override PaintingsLayoutViewHolder OnCreateViewHolder(ViewGroup container)
        {
            var holder = new PaintingsLayoutViewHolder(LayoutInflater.FromContext(container.Context).Inflate(Resource.Layout.item_painting_layout, container, false));
            holder.Layout.Controller.Settings.SetMaxZoom(MAX_ZOOM);
            holder.Layout.Controller.EnableScrollInViewPager(_viewPager);
            return holder;
        }

        public override void OnBindViewHolder(PaintingsLayoutViewHolder holder, int position)
        {
            _setupListener?.OnSetupGestureView(holder.Layout);
            holder.Layout.Controller.ResetState();

            holder.Image.SetImageResource(_paintings[position].ImageId);

            var html = "<b>" + _paintings[position].Author + "</b><br/>" + _paintings[position].Title;
            DroidUtils.SetHtmlText(holder.Title, html);

            holder.Button.Tag = _paintings[position].Link;
            holder.Button.SetOnClickListener(this);
        }

        public override void OnRecycleViewHolder(PaintingsLayoutViewHolder holder)
        {
            holder.Layout.Controller.ResetState();
            holder.Image.SetImageResource(0);
        }

        public void OnClick(View v)
        {
            var url = (string)v.Tag;
            Intent i = new Intent(Intent.ActionView);
            i.SetData(Android.Net.Uri.Parse(url));
            ((Activity)(v.Context)).StartActivity(i);
        }
    }

    public class PaintingsLayoutViewHolder : RecyclePagerAdapter.ViewHolder
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