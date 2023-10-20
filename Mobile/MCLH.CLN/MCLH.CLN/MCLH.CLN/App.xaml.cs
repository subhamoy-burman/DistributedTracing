using MCLH.CLN.Services;
using MCLH.CLN.Views;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sentry;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace MCLH.CLN
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            

            //SentrySdk.CaptureMessage("Message");
            // App code

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            
        }
    }
}
