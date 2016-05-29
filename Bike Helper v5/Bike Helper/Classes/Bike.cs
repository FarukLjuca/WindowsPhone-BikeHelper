using Bike_Helper.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Bike_Helper.Model
{
    public class Bike
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }
        public String Model { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }

        public String FullUrl
        {
            get { return AppValues.IMAGE_URL + Image; }
        }
    }
}
