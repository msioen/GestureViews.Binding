using Com.Alexvasilkov.Gestures.Views.Interfaces;

namespace Com.Alexvasilkov.Gestures.Views
{
    partial class GestureImageView
    {
        GestureController IGestureView.Controller
        {
            get { return this.Controller; }
        }
    }
}