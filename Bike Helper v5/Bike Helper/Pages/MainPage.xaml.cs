using Bike_Helper.Controls;
using Bike_Helper.Helpers;
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
using Windows.Services.Maps;
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
        private bool firstLoad = true;
        // Represents that find button is active (has been clicked, and route has been found)
        private bool findActive = false;

        private MapRouteView foundRoute;
        private StatusBar statusBar;
        private List<Store> stores;
        private Geoposition myGeoposition;

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
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            StatusBar.ProgressIndicator.Text = "Detecting current location...";
            await statusBar.ProgressIndicator.ShowAsync();

            User user = User.getInstance();
            if (user.isLoggedIn())
            {
                abLogin.Icon = new SymbolIcon(Symbol.BlockContact);
                abLogin.Label = "Logout";
            }
            else
            {
                abLogin.Icon = new SymbolIcon(Symbol.Contact);
                abLogin.Label = "Login";
            }
        }

        private async void mcMainMap_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (firstLoad)
                {
                    SetMap();
                }
                else
                {
                    await StatusBar.ProgressIndicator.HideAsync();
                }
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);
                await dialog.ShowAsync();
            }
        }

        private async void SetMap()
        {
            myGeoposition = await new Geolocator().GetGeopositionAsync();
            StatusBar.ProgressIndicator.Text = "Synchronizing with server...";
            await StatusBar.ProgressIndicator.ShowAsync();
            await mcMainMap.TrySetViewAsync(myGeoposition.Coordinate.Point, 15);

            Pushpin youPushpin = new Pushpin("You are here", myGeoposition.Coordinate.Point, null, null);
            mcMainMap.Children.Add(youPushpin);

            API api = RestService.For<API>(AppValues.BASE_URL);
            try
            {
                stores = await api.GetAllStores();
            }
            catch (Exception ex) { }

            if (stores != null)
            {
                foreach (Store store in stores)
                {
                    BasicGeoposition basicGeoposition = new BasicGeoposition();
                    basicGeoposition.Latitude = store.Latitude;
                    basicGeoposition.Longitude = store.Longitude;

                    Pushpin pushpin = new Pushpin(store.Name, new Geopoint(basicGeoposition), store, pushpin_Click);
                    mcMainMap.Children.Add(pushpin);
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Check your internet connection");
                await dialog.ShowAsync();
            }

            await StatusBar.ProgressIndicator.HideAsync();

            firstLoad = false;
        }

        void pushpin_Click(object sender, RoutedEventArgs e)
        {
            Pushpin button = sender as Pushpin;
            Frame.Navigate(typeof(DetailsPage), button.Store);
        }

        private async void Find_Click(object sender, RoutedEventArgs e)
        {
            if (firstLoad == false)
            {
                if (!findActive)
                {
                    Store closestStore = null;
                    double closestDistance = Double.MaxValue;

                    foreach (Store store in stores)
                    {
                        // Pythagorean theorem for distance between two points
                        double x1 = store.Latitude;
                        double y1 = store.Longitude;
                        double x2 = myGeoposition.Coordinate.Point.Position.Latitude;
                        double y2 = myGeoposition.Coordinate.Point.Position.Longitude;

                        double distance = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestStore = store;
                        }
                    }

                    if (closestStore != null)
                    {
                        BasicGeoposition myBasicGeoposition = new BasicGeoposition();
                        myBasicGeoposition.Latitude = myGeoposition.Coordinate.Point.Position.Latitude;
                        myBasicGeoposition.Longitude = myGeoposition.Coordinate.Point.Position.Longitude;
                        Geopoint myGeopoint = new Geopoint(myBasicGeoposition);

                        BasicGeoposition storeBasicGeoposition = new BasicGeoposition();
                        storeBasicGeoposition.Latitude = closestStore.Latitude;
                        storeBasicGeoposition.Longitude = closestStore.Longitude;
                        Geopoint storeGeopoint = new Geopoint(storeBasicGeoposition);

                        MapRouteFinderResult finder = await MapRouteFinder.GetWalkingRouteAsync(myGeopoint, storeGeopoint);
                        if (finder.Status == MapRouteFinderStatus.Success)
                        {
                            foundRoute = new MapRouteView(finder.Route);
                            mcMainMap.Routes.Add(foundRoute);
                            await mcMainMap.TrySetViewBoundsAsync(finder.Route.BoundingBox, null, MapAnimationKind.Bow);

                            abFind.Icon = new SymbolIcon(Symbol.Cancel);
                            abFind.Label = "Cancel Route";
                            findActive = true;
                        }
                    }
                }
                else
                {
                    mcMainMap.Routes.Remove(foundRoute);

                    abFind.Icon = new SymbolIcon(Symbol.Find);
                    abFind.Label = "Find Route";
                    findActive = false;
                }
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (firstLoad == false)
            {
                User user = User.getInstance();
                if (user.isLoggedIn())
                {
                    user.logout();
                    AppHelper.showToast("You are now logged out.");

                    abLogin.Icon = new SymbolIcon(Symbol.Contact);
                    abLogin.Label = "Login";
                }
                else
                {
                    Frame.Navigate(typeof(LoginPage));
                }
            }
        }
    }
}