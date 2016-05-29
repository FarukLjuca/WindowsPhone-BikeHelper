using Bike_Helper.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper
{
    interface API
    {
        [Get("/api/store")]
        Task<List<Store>> GetAllStores();

        [Get("/api/store/{id}")]
        Task<Store> GetStore(int id);

        [Get("/api/user")]
        Task<List<User>> GetAllUsers();

        [Get("/api/user/{id}")]
        Task<User> GetUser(int id);

        [Post("/api/user/login")]
        Task<Response> Login([Body] User user);

        [Get("/api/bike/{id}")]
        Task<Bike> GetBike(int id);

        [Get("/api/bike/store/{id}")]
        Task<ObservableCollection<Bike>> GetBikeByStore(int id);

        [Post("/api/bike/rent")]
        Task<Rent> RentABike([Body] Rent rent);

        [Post("/api/bike/return")]
        Task<Rent> ReturnABike([Body] Rent rent);
    }
}
