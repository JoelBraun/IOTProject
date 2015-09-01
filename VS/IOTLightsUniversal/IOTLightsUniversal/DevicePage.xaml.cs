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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IOTLightsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DevicePage : Page
    {
        public DevicePage(object parameter)
        {
            this.InitializeComponent();
            var p = parameter as AzureDataItem;
            DName.Text = p.DeviceName;
            DLocation.Text = p.Location;
            DeviceState.Header = "Device status";
            DUptime.Text = p.Uptime.ToString();
            ECost.Text = p.Cost.ToString();
        }
    }
}
