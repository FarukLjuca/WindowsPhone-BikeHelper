using Bike_Helper.Helpers;
using Bike_Helper.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Bike_Helper.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            User user = User.getInstance();
            user.Email = tbEmail.Text;
            user.Password = pbPassword.Password;

            API api = RestService.For<API>("https://bikehelper.herokuapp.com");
            Response response = await api.Login(user);

            if (response.StatusBoolean())
            {
                User newUser = await api.GetUser(response.UserId);
                user.Name = newUser.Name;
                user.Level = newUser.Level;

                AppHelper.showToast("Hello " + user.Name);

                if (user.Level == AppValues.USER_LEVEL_CUSTOMER)
                {
                    Frame.Navigate(typeof(MainPage));
                }
                else if (user.Level == AppValues.USER_LEVEL_EMPLOYEE)
                {
                    Frame.Navigate(typeof(EmployeePage));
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("Email or password are incorect");
                await dialog.ShowAsync();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
