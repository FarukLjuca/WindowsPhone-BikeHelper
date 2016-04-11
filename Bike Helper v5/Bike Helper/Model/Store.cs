using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Model
{
    public class Store
    {
        [AliasAs("id")]
        public int Id { get; set; }
        [AliasAs("type")]
        public String Type { get; set; }
        [AliasAs("latitude")]
        public float Latitude { get; set; }
        [AliasAs("longitude")]
        public float Longitude { get; set; }
        [AliasAs("name")]
        public String Name { get; set; }
        [AliasAs("description")]
        public String Description { get; set; }
        [AliasAs("image")]
        public String Image { get; set; }
    }
}
