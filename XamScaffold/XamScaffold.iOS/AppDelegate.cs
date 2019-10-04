using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XamScaffold.Models;
using XamScaffold.Services;

namespace XamScaffold.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            RegisterTypes();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        //Sample for register types
        //void RegisterTypes()
        //{
        //    App.RegisterType<ILogger, Logger>();
        //    App.RegisterType<FormsVideoLibrary.iOS.VideoPlayerRenderer>();
        //    App.RegisterType<TouchTracking.iOS.TouchEffect>();
        //    App.RegisterType<IPhotoPicker, Services.iOS.PhotoPicker>();
        //    App.BuildContainer();
        //}
        void RegisterTypes()
        {
            if (App.UseMockDataStore)
            {
                App.RegisterType<IDataStore<Item>,MockDataStore>();
            }
            else
            {
                App.RegisterType<IDataStore<Item>, AzureDataStore>();
            }
            App.BuildContainer();
        }
    }
}
