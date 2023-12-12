using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Linq;
using Sentry;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace MCLH.CLN.Views
{
    public partial class AboutPage : ContentPage
    {
        private static int _callTimes = 0;
        public AboutPage()
        {
            InitializeComponent();

            Task.Run(async() =>
            {
                await GetOperationIdAsync("https://clnbackendmhack.azurewebsites.net/api/Demo");
            }).Wait();
        }

        public async Task<string> GetOperationIdAsync(String ApiEndpoint)
        {
            SentryXamarin.Init(o =>
            {
                o.AddXamarinFormsIntegration();
                // Tells which project in Sentry to send events to:
                o.Dsn = "<insert>";
                // When configuring for the first time, to see what the SDK is doing:
                o.Debug = true;
                // Set TracesSampleRate to 1.0 to capture 100%
                // of transactions for performance monitoring.
                // We recommend adjusting this value in production
                o.TracesSampleRate = 1.0;
                o.AttachScreenshots = true;

            });
            try
            {
                using (HttpClient client = new HttpClient(new CustomHttpClientHandler()))
                {
                    _callTimes = _callTimes+1;
                    
                    // Send GET request
                    HttpResponseMessage response = await client.GetAsync(ApiEndpoint);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Extract Operation-Id from the response headers
                        if (response.Headers.TryGetValues("Operation-Id", out var operationIdValues))
                        {
                            string operationId = operationIdValues.FirstOrDefault();
                            if (_callTimes % 3 == 0)
                            {
                                SentrySdk.CaptureException(new NotImplementedException());
                                throw new NotImplementedException();

                            }
                            SentrySdk.CaptureMessage($"{operationId} : Fetched the data about My CareLink Heart device and saved in SQLite");

                            return operationId;
                        }
                        else
                        {
                            // Handle the case where Operation-Id header is not present
                            return "Operation-Id not found in headers";
                        }
                    }
                    else
                    {
                        // Handle the case where the request was not successful
                        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the request
                return $"Exception: {ex.Message}";
            }
        }
    }
}