using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

    }
}
