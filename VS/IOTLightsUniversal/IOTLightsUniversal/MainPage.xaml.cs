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
using Windows.Devices.Gpio;
using IOTLightsUniversal.DataModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IOTLightsUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        public ObservableCollection<AzureDataItem> DefaultViewModel = new ObservableCollection<AzureDataItem>();

        public MainPage()
        {
            this.InitializeComponent();
            HamburgerListItemCommand = new RelayCommand(this.HamburgerListButtonClick);
        }


        public MainPage(Frame frame)
        {
            this.InitializeComponent();
            MainSplitView.Content = frame;
            (MainSplitView.Content as Frame).Navigate(typeof(MicPage));
            getData();
            HamburgerListItemCommand = new RelayCommand(this.HamburgerListButtonClick);
            HamburgerList.ItemsSource = DefaultViewModel;
            
        }

        private async void getData()
        {
            var AzureDataItems = await AzureDataSource.GetDataItemsAsync();
            foreach (AzureDataItem adi in AzureDataItems)
            {
                DefaultViewModel.Add(adi);
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
            
        }

        private void HamburgerListButtonClick()
        {
            var item = (HamburgerButton as FrameworkElement).
            var item2 = (item as FrameworkElement).Parent;
            var item3 = (item as FrameworkElement).;
            int index = HamburgerList.Items.IndexOf(item);
            HamburgerList.SelectedIndex = index;
        }

        private void HamburgerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = HamburgerList.SelectedIndex;
            
        }

        public ICommand HamburgerListItemCommand
        {
            get;
            private set;
        }

    }
}
