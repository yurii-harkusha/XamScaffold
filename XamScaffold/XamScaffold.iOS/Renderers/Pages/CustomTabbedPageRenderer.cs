using System;
using Xamarin.Forms;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using System.Drawing;
using Foundation;
using System.Threading.Tasks;
using XamScaffold.iOS.Renderers.Pages;

[assembly: ExportRenderer(typeof(XamScaffold.Views.TabbedPage.TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace XamScaffold.iOS.Renderers.Pages
{
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (TabBar.Items == null) return;

            Xamarin.Forms.Color SelectedColor = Xamarin.Forms.Color.FromHex("#1A73E8");
            byte SelectedColorR = Convert.ToByte(255 * SelectedColor.R);
            byte SelectedColorG = Convert.ToByte(255 * SelectedColor.G);
            byte SelectedColorB = Convert.ToByte(255 * SelectedColor.B);
            var selectedUIColor = UIColor.FromRGB(SelectedColorR, SelectedColorG, SelectedColorB);

            Xamarin.Forms.Color UnselectedColor = Xamarin.Forms.Color.FromHex("#5F6267");
            byte UnselectedColorR = Convert.ToByte(255 * UnselectedColor.R);
            byte UnselectedColorG = Convert.ToByte(255 * UnselectedColor.G);
            byte UnselectedColorB = Convert.ToByte(255 * UnselectedColor.B);
            var unselectedUIColor = UIColor.FromRGB(UnselectedColorR, UnselectedColorG, UnselectedColorB);

            if (TabBar.Items != null)
            {
                foreach (var item in TabBar.Items)
                {
                    item.SetTitleTextAttributes(new UITextAttributes()
                    {
                        TextColor = unselectedUIColor,
                        Font = UIFont.FromName("Montserrat-SemiBold", 17.0F),
                        TextShadowColor = UIColor.Clear
                    }, UIControlState.Normal);
                    item.SetTitleTextAttributes(new UITextAttributes()
                    {
                        TextColor = selectedUIColor,
                        Font = UIFont.FromName("Montserrat-SemiBold", 17.0F),
                        TextShadowColor = UIColor.Clear
                    }, UIControlState.Selected);
                }
            }
        }
    }
}