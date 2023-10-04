using Autofac;
using Autofac.Features.Metadata;
using AutoFacTest.Commands;
using AutoFacTest.Repositories;
using AutoFacTest.Repositories.PokeApiNet;
using AutoFacTest.Repositories.PokeLocal;
using AutoFacTest.ViewModels;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AutoFacTest
{
    sealed partial class App : Application
    {
        public static IContainer Container { get; set; }

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            var builder = new ContainerBuilder();

            // builder.RegisterType<PokeLocalRepository>().As<IPokeRepository>().SingleInstance();
            builder.RegisterType<PokeApiNetRepository>().As<IPokeRepository>();
            builder.RegisterType<MainPageViewModel>().OnActivated(e => e.Instance.LoadPokemons());

            builder.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save Pokemon");
            builder.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open Pokemon");
            builder.RegisterAdapter<Meta<ICommand>, IToolbarButton>(cmd => new ToolbarButton(cmd.Value, (string)cmd.Metadata["Name"]));

            Container = builder.Build();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
