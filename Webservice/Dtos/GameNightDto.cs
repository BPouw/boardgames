using System;
using Core.Domain;
namespace Webservice.Dtos
{
    public class GameNightDto
    {
        // snake case because we will be returning json

        public string name { get; set; }
        public DateTime game_time { get; set; }
        public Address address { get; set; }
        public PersonDto organiser { get; set; }
        public int max_players { get; set; }
        public bool adults_only { get; set; }
        public Boolean vegan { get; set; }
        public Boolean lactose_intolerant { get; set; }
        public Boolean nut_allergy { get; set; }
        public Boolean alcohol_free { get; set; }

    }
}

