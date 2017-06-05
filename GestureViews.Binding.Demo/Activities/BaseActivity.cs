using Android.Support.V7.App;
using Android.Views;

namespace GestureViews.Binding.Demo.Activities
{
    public class BaseActivity : AppCompatActivity
    {
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}