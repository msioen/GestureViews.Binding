using System;

namespace GestureViews.Binding.Demo.Utils
{
    public class Painting
    {
        public int ImageId { get; }
        public string Author { get; }
        public string Title { get; }
        public string Link { get; }

        public Painting(int imageId, string author, string title, string link)
        {
            ImageId = imageId;
            Author = author;
            Title = title;
            Link = link;
        }

    }
}