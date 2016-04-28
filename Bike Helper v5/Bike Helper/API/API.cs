using Bike_Helper.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper
{
    interface API
    {
        [Get("/api/store")]
        Task<List<Store>> GetStore();

        [Post("/api/user/login")]
        Task<Response> Login([Body] User user);

        [Get("/api/user/{id}")]
        Task<User> GetUser(int id);

        [Get("/api/bike/store/{id}")]
        Task<List<Bike>> GetBikeByStore(int id);
    }
}
