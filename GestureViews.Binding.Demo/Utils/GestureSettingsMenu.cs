using Android.Views;
using Com.Alexvasilkov.Gestures;
using Com.Alexvasilkov.Gestures.Internal;
using Com.Alexvasilkov.Gestures.Views.Interfaces;
using System;

namespace GestureViews.Binding.Demo.Utils
{
    public class GestureSettingsMenu : IGestureSettingsSetupListener
    {
        private const float OVERSCROLL = 32f;
        private const long SLOW_ANIMATIONS = 1500L;

        private bool isPanEnabled = true;
        private bool isZoomEnabled = true;
        private bool isRotationEnabled = false;
        private bool isRestrictRotation = false;
        private bool isOverscrollXEnabled = false;
        private bool isOverscrollYEnabled = false;
        private bool isOverzoomEnabled = true;
        private bool isExitEnabled = true;
        private bool isFillViewport = true;
        private Settings.Fit fitMethod = Settings.Fit.Inside;
        private int gravity = (int)GravityFlags.Center;
        private bool isSlow = false;

        public void OnSetupGestureView(IGestureView view)
        {
            var context = ((View)view).Context;

            float overscrollX = isOverscrollXEnabled ? OVERSCROLL : 0f;
            float overscrollY = isOverscrollYEnabled ? OVERSCROLL : 0f;
            float overzoom = isOverzoomEnabled ? 2f : 1f;

            view.Controller.Settings
                    .SetPanEnabled(isPanEnabled)
                    .SetZoomEnabled(isZoomEnabled)
                    .SetDoubleTapEnabled(isZoomEnabled)
                    .SetRotationEnabled(isRotationEnabled)
                    .SetRestrictRotation(isRestrictRotation)
                    .SetOverscrollDistance(context, overscrollX, overscrollY)
                    .SetOverzoomFactor(overzoom)
                    .SetExitEnabled(isExitEnabled)
                    .SetFillViewport(isFillViewport)
                    .SetFitMethod(fitMethod)
                    .SetGravity(gravity)
                    .SetAnimationsDuration(isSlow ? SLOW_ANIMATIONS : 300L);
        }

        public bool OnCreateOptionsMenu(IMenu menu)
        {
            AddBoolMenu(menu, isPanEnabled, Resource.String.menu_enable_pan);
            AddBoolMenu(menu, isZoomEnabled, Resource.String.menu_enable_zoom);
            AddBoolMenu(menu, isRotationEnabled, Resource.String.menu_enable_rotation);
            AddBoolMenu(menu, isRestrictRotation, Resource.String.menu_restrict_rotation);
            AddBoolMenu(menu, isOverscrollXEnabled, Resource.String.menu_enable_overscroll_x);
            AddBoolMenu(menu, isOverscrollYEnabled, Resource.String.menu_enable_overscroll_y);
            AddBoolMenu(menu, isOverzoomEnabled, Resource.String.menu_enable_overzoom);
            AddBoolMenu(menu, isExitEnabled, Resource.String.menu_enable_exit);
            AddBoolMenu(menu, isFillViewport, Resource.String.menu_fill_viewport);
            AddSubMenu(menu, Settings.Fit.Values(), fitMethod, Resource.String.menu_fit_method);
            AddSubMenu(menu, (EGravityType[])Enum.GetValues(typeof(EGravityType)), FindGravityType(gravity), Resource.String.menu_gravity);
            AddBoolMenu(menu, isSlow, Resource.String.menu_enable_slow);
            AddBoolMenu(menu, GestureDebug.DrawDebugOverlay, Resource.String.menu_enable_overlay);
            return true;
        }

        void AddBoolMenu(IMenu menu, bool isChecked, int titleId)
        {
            var item = menu.Add(Menu.None, titleId, 0, titleId);
            item.SetCheckable(true);
            item.SetChecked(isChecked);
        }

        void AddSubMenu<T>(IMenu menu, T[] items, T selected, int titleId)
        {
            var sub = menu.AddSubMenu(titleId);
            sub.SetGroupCheckable(Menu.None, true, true);

            for (int i = 0; i < items.Length; i++)
            {
                var item = sub.Add(Menu.None, titleId, i, items[i].ToString());
                item.SetCheckable(true);
                item.SetChecked(Equals(items[i], selected));
            }
        }

        public bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.String.menu_enable_pan:
                    isPanEnabled = !isPanEnabled;
                    break;
                case Resource.String.menu_enable_zoom:
                    isZoomEnabled = !isZoomEnabled;
                    break;
                case Resource.String.menu_enable_rotation:
                    isRotationEnabled = !isRotationEnabled;
                    break;
                case Resource.String.menu_restrict_rotation:
                    isRestrictRotation = !isRestrictRotation;
                    break;
                case Resource.String.menu_enable_overscroll_x:
                    isOverscrollXEnabled = !isOverscrollXEnabled;
                    break;
                case Resource.String.menu_enable_overscroll_y:
                    isOverscrollYEnabled = !isOverscrollYEnabled;
                    break;
                case Resource.String.menu_enable_overzoom:
                    isOverzoomEnabled = !isOverzoomEnabled;
                    break;
                case Resource.String.menu_enable_exit:
                    isExitEnabled = !isExitEnabled;
                    break;
                case Resource.String.menu_fill_viewport:
                    isFillViewport = !isFillViewport;
                    break;
                case Resource.String.menu_fit_method:
                    fitMethod = Settings.Fit.Values()[item.Order];
                    break;
                case Resource.String.menu_gravity:
                    gravity = (int)(((EGravityType[])Enum.GetValues(typeof(EGravityType)))[item.Order]);
                    break;
                case Resource.String.menu_enable_slow:
                    isSlow = !isSlow;
                    break;
                case Resource.String.menu_enable_overlay:
                    GestureDebug.DrawDebugOverlay = !GestureDebug.DrawDebugOverlay;
                    break;
                default:
                    return false;
            }

            return true;
        }

        EGravityType FindGravityType(int gravity)
        {
            foreach (EGravityType value in Enum.GetValues(typeof(EGravityType)))
            {
                if ((int)value == gravity)
                    return value;
            }
            throw new NotSupportedException();
        }

        enum EGravityType
        {
            Center = GravityFlags.Center,
            Top = GravityFlags.Top,
            Bottom = GravityFlags.Bottom,
            Start = GravityFlags.Start,
            End = GravityFlags.End,
            TopStart = GravityFlags.Top | GravityFlags.Start,
            BottomEnd = GravityFlags.Bottom | GravityFlags.End
        }
    }
}