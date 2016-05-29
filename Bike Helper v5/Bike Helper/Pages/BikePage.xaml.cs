using Bike_Helper.Helpers;
using Bike_Helper.Model;
using Refit;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Bike_Helper.Pages
{
    public sealed partial class BikePage : Page
    {
        private Bike bike;

        public BikePage()
        {
            this.InitializeComponent();
        }
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            int? id = e.Parameter as int?;
            if (id != null)
            {
                API api = RestService.For<API>(AppValues.BASE_URL);
                bike = await api.GetBike((int) id);

                imImage.Source = new BitmapImage(new Uri(AppValues.IMAGE_URL + bike.Image));
                tbModel.Text = bike.Model;
                tbDescription.Text = bike.Description;
                if (bike.StoreId != null)
                {
                    Store store = await api.GetStore((int)bike.StoreId);
                    tbLokacija.Text = "Lokacija:\n Biciklo je u prodavnici " + store.Name;
                }
                else if (bike.UserId != null)
                {
                    User user = await api.GetUser((int)bike.UserId);
                    tbLokacija.Text = "Lokacija:\n Bicklo je kod korisnika " + user.Name;
                }
            }
        }
    }
}
