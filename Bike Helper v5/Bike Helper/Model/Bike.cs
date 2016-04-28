using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Model
{
    public class Bike
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int? StoreId { get; set; }
        public int? UserId { get; set; }
        public String Model { get; set; }
        public String Image { get; set; }
    }
}
