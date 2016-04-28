using Bike_Helper.Helpers;
using Bike_Helper.Model;
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

namespace Bike_Helper.View
{
    public sealed partial class EmployeePage : Page
    {
        User user;

        public EmployeePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            user = User.getInstance();
            txInfo.Text = user.ToString();
        }

        private void RentABike_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnABike_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            user.logout();
            AppHelper.showToast("You are now logged out");
            Frame.Navigate(typeof(MainPage));
            Frame.BackStack.RemoveAt(Frame.BackStack.Count - 1);
        }
    }
}
