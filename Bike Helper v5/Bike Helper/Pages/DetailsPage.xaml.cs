using Bike_Helper.Helpers;
using Bike_Helper.Model;
using Bike_Helper.Pages;
using Refit;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Bike_Helper.View
{
    public sealed partial class DetailsPage : Page
    {
        private Store store;
        public ObservableCollection<Bike> Bikes { get; set; }

        public DetailsPage()
        {
            this.InitializeComponent();
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            store = e.Parameter as Store;
            imImage.Source = new BitmapImage(new Uri(store.Image));
            tbTitle.Text = store.Name;
            tbDescription.Text = store.Description;

            API api = RestService.For<API>(AppValues.BASE_URL);
            Bikes = await api.GetBikeByStore(store.Id);
            lvBikes.DataContext = Bikes;
        }

        private void Card_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            int id = (int)(sender as Grid).Tag;
            Frame.Navigate(typeof(BikePage), id);
        }
    }
}
