using System;
namespace Webservice.Dtos
{
    public class AddressDto
    {
        public string street_name { get; set; }
        public int house_number { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
    }
}

