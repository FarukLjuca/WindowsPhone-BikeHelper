using Bike_Helper.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper
{
    interface IStoreAPI
    {
        [Get("/api/store")]
        Task<List<Store>> GetStore();
    }
}
