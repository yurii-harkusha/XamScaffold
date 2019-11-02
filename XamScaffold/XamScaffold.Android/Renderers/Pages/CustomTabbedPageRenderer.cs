using System.ComponentModel;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using XamScaffold.Droid.Renderers.Pages;

[assembly: ExportRenderer(typeof(XamScaffold.Views.TabbedPage.TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace XamScaffold.Droid.Renderers.Pages
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        Context _context;
        ColorStateList bottomTabBarItemsStateList;

        public CustomTabbedPageRenderer(Context context) : base(context)
        {
            _context = context;
            InitializeBottomTabBarItemsStateList();
        }

        public void InitializeBottomTabBarItemsStateList()
        {
            var selectedColor = new Android.Graphics.Color(ContextCompat.GetColor(_context, Resource.Color.tabsSelectedColor));
            var unselectedColor = new Android.Graphics.Color(ContextCompat.GetColor(_context, Resource.Color.tabsUnselectedColor));
            bottomTabBarItemsStateList = new ColorStateList(
            new int[][]
                {
                new int[] { Android.Resource.Attribute.StateChecked
                        },
                        new int[] { Android.Resource.Attribute.StateEnabled
                        }
            },
            new int[] {
                selectedColor,
                unselectedColor
            });
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "CurrentPage")
            {
                UpgradeTabbedItemsAppereance();
            }

            if (!(GetChildAt(0) is ViewGroup layout))
                return;

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
                return;

            var topShadow = LayoutInflater.From(Context).Inflate(Resource.Layout.view_shadow, null);

            var layoutParams =
                new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, 15);
            layoutParams.AddRule(LayoutRules.Above, bottomNavigationView.Id);

            layout.AddView(topShadow, 2, layoutParams);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            UpgradeTabbedItemsAppereance();
        }

        private void UpgradeTabbedItemsAppereance()
        {
            if (bottomTabBarItemsStateList == null)
            {
                InitializeBottomTabBarItemsStateList();
            }

            if (ViewGroup != null && ViewGroup.ChildCount > 0)
            {
                BottomNavigationView bottomNavigationView = FindChildOfType<BottomNavigationView>(ViewGroup);
                bottomNavigationView.SetShiftMode(false, false);
                BottomNavigationMenuView bottomNavigationMenuView = FindChildOfType<BottomNavigationMenuView>(ViewGroup);

                for (int i = 0; i < bottomNavigationMenuView.ChildCount; i++)
                {
                    BottomNavigationItemView item = (BottomNavigationItemView)bottomNavigationMenuView.GetChildAt(i);
                    item.SetTextColor(bottomTabBarItemsStateList);
                    item.SetShifting(false);
                    item.SetLabelVisibilityMode(LabelVisibilityMode.LabelVisibilityLabeled);
                    item.SetChecked(item.ItemData.IsChecked);

                    Android.Views.View activeLabel = item.FindViewById(Resource.Id.largeLabel);
                    if (activeLabel is TextView)
                    {
                        var font = Typeface.CreateFromAsset(_context.Assets, "Montserrat-SemiBold.ttf");
                        (activeLabel as TextView).Typeface = font;
                    }

                    Android.Views.View inActiveLabel = item.FindViewById(Resource.Id.smallLabel);
                    if (activeLabel is TextView)
                    {
                        var font = Typeface.CreateFromAsset(_context.Assets, "Montserrat-SemiBold.ttf");
                        (inActiveLabel as TextView).Typeface = font;
                    }
                }
            }

            T FindChildOfType<T>(ViewGroup viewGroup) where T : Android.Views.View
            {
                if (viewGroup == null || viewGroup.ChildCount == 0) return null;

                for (var i = 0; i < viewGroup.ChildCount; i++)
                {
                    var child = viewGroup.GetChildAt(i);

                    var typedChild = child as T;
                    if (typedChild != null) return typedChild;

                    if (!(child is ViewGroup)) continue;

                    var result = FindChildOfType<T>(child as ViewGroup);

                    if (result != null) return result;
                }

                return null;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            UpgradeTabbedItemsAppereance();
        }
    }
}