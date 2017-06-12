using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Alexvasilkov.Gestures;
using Com.Alexvasilkov.Gestures.Views.Interfaces;
using System;

namespace GestureViews.Binding.Demo.Views
{
    [Register("gestureviews.binding.demo.views.GestureTextView")]
    public class GestureTextView : TextView, IGestureView, GestureController.IOnStateChangeListener
    {
        float _origSize;
        float _size;

        public GestureController Controller { get; }

        public GestureTextView(Context context)
            : this(context, null, 0) { }

        public GestureTextView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0) { }

        public GestureTextView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Controller = new GestureController(this);
            Controller.Settings.SetOverzoomFactor(1f).SetPanEnabled(false);
            Controller.AddOnStateChangeListener(this);

            _origSize = TextSize;
        }

        public void OnStateChanged(State state)
        {
            ApplyState(state);
        }

        public void OnStateReset(State oldState, State newState)
        {
            ApplyState(newState);
        }

        protected void ApplyState(State state)
        {
            var size = _origSize * state.Zoom;
            var maxZoom = Controller.Settings.MaxZoom;
            size = Math.Max(_origSize, Math.Min(size, _origSize * maxZoom));

            if (!State.Equals(_size, size))
            {
                _size = size;
                base.SetTextSize(ComplexUnitType.Px, size);
            }
        }

        public override void SetTextSize([GeneratedEnum] ComplexUnitType unit, float size)
        {
            base.SetTextSize(unit, size);
            _origSize = TextSize;
            ApplyState(Controller.State);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            Controller.Settings.SetViewport(w, h).SetImage(w, h);
            Controller.UpdateState();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.PointerDown)
            {
                if (Parent != null)
                {
                    Parent.RequestDisallowInterceptTouchEvent(true);
                }
            }

            return Controller.OnTouch(this, e);
        }
    }
}