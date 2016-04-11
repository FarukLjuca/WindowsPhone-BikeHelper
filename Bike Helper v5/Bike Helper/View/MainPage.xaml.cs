using Bike_Helper.Controls;
using Bike_Helper.Model;
using Bike_Helper.View;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Bike_Helper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StatusBar statusBar;

        public StatusBar StatusBar
        {
            get
            {
                if (statusBar == null)
                {
                    statusBar = StatusBar.GetForCurrentView();
                }
                return statusBar;
            }
        }


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            StatusBar.ProgressIndicator.Text = "Detecting current location...";
            await statusBar.ProgressIndicator.ShowAsync();
        }

        private void mcMainMap_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SetMap();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);
                dialog.ShowAsync();
            }
        }

        private async void SetMap()
        {
            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = await geolocator.GetGeopositionAsync();
            await StatusBar.ProgressIndicator.HideAsync();
            await mcMainMap.TrySetViewAsync(geoposition.Coordinate.Point, 15);

            //Todo styling
            Pushpin youPushpin = new Pushpin();
            TextBlock youTextBlock = new TextBlock();

            youTextBlock.Text = "You are here";
            youPushpin.Content = youTextBlock;

            MapControl.SetLocation(youPushpin, geoposition.Coordinate.Point);
            MapControl.SetNormalizedAnchorPoint(youPushpin, new Point(0.5, 0.5));
            mcMainMap.Children.Add(youPushpin);

            IStoreAPI storeAPI = RestService.For<IStoreAPI>("https://bikehelper.herokuapp.com");
            List<Store> storeList = await storeAPI.GetStore();

            foreach (Store store in storeList)
            {
                Pushpin pushpin = new Pushpin();
                pushpin.Store = store;
                TextBlock storeName = new TextBlock();
                storeName.Text = store.Name;
                pushpin.Content = storeName;
                pushpin.Click += pushpin_Click;

                BasicGeoposition basicGeoposition = new BasicGeoposition();
                basicGeoposition.Latitude = store.Latitude;
                basicGeoposition.Longitude = store.Longitude;

                MapControl.SetLocation(pushpin, new Geopoint(basicGeoposition));
                MapControl.SetNormalizedAnchorPoint(pushpin, new Point(0.5, 0.5));
                mcMainMap.Children.Add(pushpin);
            }
        }

        void pushpin_Click(object sender, RoutedEventArgs e)
        {
            Pushpin button = sender as Pushpin;
            Frame.Navigate(typeof(DetailsPage), button.Store);
        }
    }
}