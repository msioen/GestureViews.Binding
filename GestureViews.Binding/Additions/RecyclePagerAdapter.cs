using Android.Support.V4.View;
using Android.Views;
using System.Collections.Generic;
using static Com.Alexvasilkov.Gestures.Commons.RecyclePagerAdapter;

namespace Com.Alexvasilkov.Gestures.Commons
{
    public abstract class RecyclePagerAdapter<T> : PagerAdapter where T : RecyclePagerAdapter.ViewHolder
    {
        Queue<T> _cache = new Queue<T>();
        Dictionary<int, T> _attached = new Dictionary<int, T>();

        public abstract T OnCreateViewHolder(ViewGroup container);
        public abstract void OnBindViewHolder(T holder, int position);

        public virtual void OnRecycleViewHolder(T holder)
        {
        }

        public T GetViewHolder(int position) { return _attached[position]; }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            T holder = null;
            if (_cache.Count > 0)
            {
                holder = _cache.Dequeue();
            }
            if (holder == null)
            {
                holder = OnCreateViewHolder(container);
            }
            _attached[position] = holder;

            container.AddView(holder.ItemView, null);

            OnBindViewHolder(holder, position);
            return holder;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            T holder = (T)@object;
            _attached.Remove(position);
            container.RemoveView(holder.ItemView);
            _cache.Enqueue(holder);
            OnRecycleViewHolder(holder);
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            ViewHolder holder = (ViewHolder)@object;
            return holder.ItemView == view;
        }

        public override int GetItemPosition(Java.Lang.Object @object)
        {
            return PositionNone;
        }
    }
}