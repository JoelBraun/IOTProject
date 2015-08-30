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
using IOTLightsUniversal.Data;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IOTLightsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public MainPage(Frame frame)
        {
            this.InitializeComponent();
            MainSplitView.Content = frame;
            (MainSplitView.Content as Frame).Navigate(typeof(HomePage));
            getData();
        }

        private async void getData()
        {
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }
        /*
        private void Item1Click(object sender, RoutedEventArgs e)
        {
            (MainSplitView.Content as Frame).Navigate(typeof(HomePage));
            HamburgerList.SelectedItem = Home;
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private void Item2Click(object sender, RoutedEventArgs e)
        {
            (MainSplitView.Content as Frame).Navigate(typeof(MicPage));
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
            HamburgerList.SelectedItem = Microphone;
        }
        */
    }
}
