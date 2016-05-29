using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bike_Helper.Model
{
    public class Response
    {
        public int UserId { get; set; }
        public char Status { get; set; }

        public bool StatusBoolean()
        {
            if (Status == 't') return true;
            return false;
        }
    }
}
