using Bike_Helper.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Model
{
    public class User
    {
        private static User instance;
        public int? Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public int Level { get; set; }

        private User() { }

        public static User getInstance()
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }

        public bool isLoggedIn()
        {
            return Id != null;
        }

        public void logout()
        {
            Id = null;
        }

        public override String ToString()
        {
            return Name;
        }

        public String getDetails()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Full name: ");
            result.Append(Name);
            result.Append("\nEmail: ");
            result.Append(Email);
            result.Append("\nUser level: ");
            switch (Level)
            {
                case AppValues.USER_LEVEL_CUSTOMER:
                    result.Append("Customer");
                    break;
                case AppValues.USER_LEVEL_EMPLOYEE:
                    result.Append("Employee");
                    break;
                default:
                    result.Append("Not defined");
                    break;
            }
            return result.ToString();
        }
    }
}
