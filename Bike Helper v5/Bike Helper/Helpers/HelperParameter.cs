using Bike_Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Helpers
{
    /// <summary>
    /// This class is used to pass two int variables as parameters from one page to another
    /// </summary>
    class HelperParameter
    {
        public User User { get; set; }
        public Store Store { get; set; }

        public HelperParameter(User user, Store store)
        {
            User = user;
            Store = store;
        }
    }
}
