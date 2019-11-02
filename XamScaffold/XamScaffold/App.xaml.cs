using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using XamScaffold.Services;
using XamScaffold.Views;
using DeviceInfo = Xamarin.Essentials.DeviceInfo;

namespace XamScaffold
{
    public partial class App : Application
    {
        static IContainer container;
        static readonly ContainerBuilder builder = new ContainerBuilder();

        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl = DeviceInfo.Platform ==
            DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";

        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();

            DependencyResolver.ResolveUsing(type => container.IsRegistered(type) ? container.Resolve(type) : null);
            MainPage = new Views.MasterDetail.MasterDetailPage();
        }

        public static void RegisterType<T>() where T : class
        {
            builder.RegisterType<T>();
        }

        public static void RegisterType<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>().As<TInterface>();
        }

        public static void RegisterTypeWithParameters<T>(Type param1Type, object param1Value, Type param2Type, string param2Name) where T : class
        {
            builder.RegisterType<T>()
                   .WithParameters(new List<Parameter>()
            {
                new TypedParameter(param1Type, param1Value),
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                    (pi, ctx) => ctx.Resolve(param2Type))
            });
        }

        public static void RegisterTypeWithParameters<TInterface, T>(Type param1Type, object param1Value, Type param2Type, string param2Name) where TInterface : class where T : class, TInterface
        {
            builder.RegisterType<T>()
                   .WithParameters(new List<Parameter>()
            {
                new TypedParameter(param1Type, param1Value),
                new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                    (pi, ctx) => ctx.Resolve(param2Type))
            }).As<TInterface>();
        }

        public static void BuildContainer()
        {
            container = builder.Build();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
