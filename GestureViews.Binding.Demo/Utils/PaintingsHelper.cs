using Android.Content.Res;
using System;

namespace GestureViews.Binding.Demo.Utils
{
    public class PaintingsHelper
    {
        static Painting[] _paintings;

        private PaintingsHelper() { }

        public static Painting[] GetPaintings(Resources res)
        {
            if (_paintings == null)
            {
                var authors = res.GetStringArray(Resource.Array.paintings_authors);
                var titles = res.GetStringArray(Resource.Array.paintings_titles);
                var links = res.GetStringArray(Resource.Array.paintings_links);
                TypedArray images = res.ObtainTypedArray(Resource.Array.paintings_images);

                int size = titles.Length;
                _paintings = new Painting[size];

                for (int i = 0; i < size; i++)
                {
                    int imageId = images.GetResourceId(i, -1);
                    _paintings[i] = new Painting(imageId, authors[i], titles[i], links[i]);
                }

                images.Recycle();
            }

            return _paintings;
        }

    }
}