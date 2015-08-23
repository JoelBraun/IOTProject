using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace IOTLights
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Generate a SAS key with the Signature Generator.: https://github.com/sandrinodimattia/RedDog/releases
            var sas = "SharedAccessSignature sr=https%3a%2f%2fjoeliot.servicebus.windows.net%2fhub1%2fpublishers%2ftestdev1%2fmessages&sig=miOuWFNgj2FNfEbCaVMGGS5qNGtOdApiQ7gQAwDR8wQ%3d&se=1440652646&skn=Policy1";

            // Namespace info.
            var serviceNamespace = "joeliot";
            var hubName = "hub1";
            var deviceName = "TestDev1";

            // Create client.
            var httpClient = new HttpClient();
            httpClient.BaseAddress =
                       new Uri(String.Format("https://{0}.servicebus.windows.net/", serviceNamespace));
            httpClient.DefaultRequestHeaders
                       .TryAddWithoutValidation("Authorization", sas);


            // Keep sending.
            while (true)
            {
                var eventData = new
                {
                    Temperature = new Random().Next(20, 50)
                };

                var postResult = httpClient.PostAsJsonAsync(
                       String.Format("{0}/publishers/{1}/messages", hubName, deviceName), eventData).Result;


                //Thread.Sleep(new Random().Next(1000, 5000));

                Task.Delay(new Random().Next(1000, 5000));


            }
        }
    }
}
