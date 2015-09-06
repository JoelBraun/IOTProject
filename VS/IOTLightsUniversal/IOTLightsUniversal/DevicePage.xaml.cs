using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IOTLightsUniversal.Common;
using IOTLightsUniversal.DataModel;
using System.Net.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IOTLightsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>


    public sealed partial class DevicePage : Page
    {
        AzureDataItem Item;
        public DevicePage(object parameter)
        {
            this.InitializeComponent();
            var p = parameter as AzureDataItem;
            Item = p;
            DName.Text = p.DeviceName;
            DLocation.Text = p.Location;
            DeviceState.Header = "Device status";
            DUptime.Text = p.Uptime.ToString();
            ECost.Text = p.Cost.ToString();
        }

        private void DeviceState_Toggled(object sender, RoutedEventArgs e)
        {
            // Generate a SAS key with the Signature Generator.: https://github.com/sandrinodimattia/RedDog/releases
            var sas = "";

            // Namespace info.
            var serviceNamespace = "joeliot";
            var hubName = "hub1";
            var deviceName = "device";

            // Create client.
            var httpClient = new HttpClient();
            httpClient.BaseAddress =
                       new Uri(String.Format("https://{0}.servicebus.windows.net/", serviceNamespace));
            httpClient.DefaultRequestHeaders
                       .TryAddWithoutValidation("Authorization", sas);

                var eventData = new
                {
                    Temperature = new Random().Next(20, 50)
                };


                var postResult = httpClient.PostAsJsonAsync(
                       String.Format("{0}/publishers/{1}/messages", hubName, deviceName), Item).Result;


                DUptime.Text = postResult.StatusCode.ToString();
            ECost.Text = postResult.Content.ReadAsStringAsync().Result;


        }
    }
}
