using Bike_Helper.Helpers;
using Bike_Helper.Model;
using Refit;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Bike_Helper.View
{
    public sealed partial class LoginPage : Page
    {
        private bool loading = false;

        public LoginPage()
        {
            this.InitializeComponent();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (loading == false)
            {
                loading = true;
                User user = User.getInstance();
                user.Email = tbEmail.Text;
                user.Password = pbPassword.Password;

                API api = RestService.For<API>(AppValues.BASE_URL);
                Response response = null;
                try
                {
                    response = await api.Login(user);
                }
                catch (Exception ex)
                {

                }

                if (response == null)
                {
                    MessageDialog dialog = new MessageDialog("Check your internet connection");
                    await dialog.ShowAsync();
                }
                else if (response.StatusBoolean())
                {
                    User newUser = await api.GetUser(response.UserId);
                    user.Id = newUser.Id;
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
                loading = false;
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
