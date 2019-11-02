using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XamScaffold.Services;
using XamScaffold.Models;

namespace XamScaffold.Droid
{
    [Activity(Label = "XamScaffold", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            RegisterTypes();
            LoadApplication(new App());
        }

        //Sample for register types
        //void RegisterTypes()
        //{
        //    App.RegisterType<ILogger, Logger>();
        //    App.RegisterTypeWithParameters<FormsVideoLibrary.Droid.VideoPlayerRenderer>(typeof(Android.Content.Context), this, typeof(ILogger), "logger");
        //    App.RegisterType<TouchTracking.Droid.TouchEffect>();
        //    App.RegisterTypeWithParameters<IPhotoPicker, Services.Droid.PhotoPicker>(typeof(Android.Content.Context), this, typeof(ILogger), "logger");
        //    App.BuildContainer();
        //}

        void RegisterTypes()
        {
            if (App.UseMockDataStore)
            {
                App.RegisterType<IDataStore<Item>, MockDataStore>();
            }
            else
            {
                App.RegisterType<IDataStore<Item>, AzureDataStore>();
            }
            App.BuildContainer();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}