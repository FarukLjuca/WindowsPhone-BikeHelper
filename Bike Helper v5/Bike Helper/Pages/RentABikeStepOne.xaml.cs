using Bike_Helper.Helpers;
using Bike_Helper.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Bike_Helper.Pages
{
    public sealed partial class RentABikeStepOne : Page
    {
        public List<User> Users { get; set; }
        public List<Store> Stores { get; set; }
        private Border selectedBorder;

        public RentABikeStepOne()
        {
            this.InitializeComponent();

            refresh();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void refresh()
        {
            API api = RestService.For<API>(AppValues.BASE_URL);
            Users = await api.GetAllUsers();
            Stores = await api.GetAllStores();

            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Level == AppValues.USER_LEVEL_EMPLOYEE)
                {
                    Users.RemoveAt(i);
                    i--;
                }
            }

            cbbUser.DataContext = Users;
            lvStore.DataContext = Stores;
        }

        private void Card_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (selectedBorder != null)
            {
                selectedBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            selectedBorder = sender as Border;
            selectedBorder.BorderBrush = new SolidColorBrush(Colors.White);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            HelperParameter parameter = new HelperParameter(cbbUser.SelectedItem as User, selectedBorder.Tag as Store);
            Frame.Navigate(typeof(RentABikeStepTwo), parameter);
        }
    }
}
