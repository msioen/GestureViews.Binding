[![NuGet Status](http://img.shields.io/nuget/v/GestureViews.Binding.svg?style=flat)](https://www.nuget.org/packages/GestureViews.Binding/)

# GestureViews binding

Android Xamarin binding for libary https://github.com/alexvasilkov/GestureViews


ImageView and FrameLayout with gestures control and position animation.

Main goal of this library is to make images viewing process as smooth as possible and to make it
easier for developers to integrate it into their apps.

#### Features ####

- Gestures support: pan, zoom, quick scale, fling, double tap, rotation.
- [Seamless integration](https://github.com/alexvasilkov/GestureViews/wiki/Usage#viewpager) with ViewPager (panning smoothly turns into ViewPager flipping and vise versa).
- ~~[View position animation](https://github.com/alexvasilkov/GestureViews/wiki/Basic-animations) ("opening" animation). Useful to animate into full image view mode.~~
- ~~[Advanced animation](https://github.com/alexvasilkov/GestureViews/wiki/Advanced-animations) from RecyclerView (or ListView) into ViewPager.~~
- Exit full image mode by scroll and scale gestures.
- Rounded images with animations support.
- [Image cropping](https://github.com/alexvasilkov/GestureViews/wiki/Image-cropping) (supports rotation).
- [Lots of settings](https://github.com/alexvasilkov/GestureViews/wiki/Settings).
- [Gestures listener](https://github.com/alexvasilkov/GestureViews/wiki/Usage#listeners): down (touch), up (touch), single tap, double tap, long press.
- Custom state animation (animating position, zoom, rotation).
- Supports both ImageView and FrameLayout out of the box, also supports [custom views](https://github.com/alexvasilkov/GestureViews/wiki/Custom-views).


Striped-through items in this list are available in the original java code but not yet through the binding.
Currently version of the binding strips out the transition part of the original java code. I'm planning to have another look at this to get this featurecomplete.
