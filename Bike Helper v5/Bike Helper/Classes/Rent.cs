using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Model
{
    public class Rent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int BikeId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public Rent(int id, int userId, int storeId, int bikeId, DateTime rentDate, DateTime returnDate)
        {
            Id = id;
            UserId = userId;
            StoreId = storeId;
            BikeId = bikeId;
            RentDate = rentDate;
            ReturnDate = returnDate;
        }
    }
}
