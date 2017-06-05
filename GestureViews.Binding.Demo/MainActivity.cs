using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;

namespace GestureViews.Binding.Demo
{
    [Activity(
        Label = "Gesture Views Demo", 
        MainLauncher = true, 
        Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        SampleAdapter _adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.main_list);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            _adapter = new SampleAdapter(GetActivitiesList(), OnItemClicked, PackageManager);
            recyclerView.SetAdapter(_adapter);
        }

        List<ActivityInfo> GetActivitiesList()
        {
            var list = new List<ActivityInfo>();

            var mainIntent = new Intent(Intent.ActionMain, null);
            mainIntent.AddCategory(Intent.CategorySampleCode);

            var packageName = ApplicationInfo.PackageName;
            var resolveList = PackageManager.QueryIntentActivities(mainIntent, 0);
            if (resolveList == null)
            {
                return list;
            }

            foreach (ResolveInfo info in resolveList)
            {
                if (string.Equals(packageName, info.ActivityInfo.PackageName, System.StringComparison.Ordinal))
                {
                    list.Add(info.ActivityInfo);
                }
            }

            return list;
        }

        void OnItemClicked(int index)
        {
            OnItemClicked(_adapter.List[index]);
        }

        void OnItemClicked(ActivityInfo info)
        {
            Intent intent = new Intent();
            intent.SetComponent(new ComponentName(this, info.Name));
            StartActivity(intent);
        }

        class SampleAdapter : RecyclerView.Adapter
        {
            Action<int> _itemClick;
            List<ActivityInfo> _list;
            PackageManager _packageManager;

            public SampleAdapter(List<ActivityInfo> list, Action<int> itemClick, PackageManager packageManager)
            {
                _list = list;
                _itemClick = itemClick;
                _packageManager = packageManager;
            }

            public List<ActivityInfo> List { get { return _list; } }

            public override int ItemCount
            {
                get { return _list.Count; }
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_main, parent, false);
                var vh = new SampleViewHolder(itemView, _itemClick);
                return vh;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                ((SampleViewHolder)holder).Title.Text = _list[position].LoadLabel(_packageManager);
            }
        }

        class SampleViewHolder : RecyclerView.ViewHolder
        {
            public TextView Title { get; }

            public SampleViewHolder(View itemView, Action<int> itemClick)
                : base(itemView)
            {
                Title = itemView.FindViewById<TextView>(Resource.Id.title);
                itemView.Click += (s, e) => itemClick?.Invoke(base.AdapterPosition);
            }
        }

    }
}

