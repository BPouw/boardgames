using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class GameNight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public Address Address { get; set; }
        public Person[] Players { get; set; }
        public int MaxPlayers { get; set; }
        public Game[] Games { get; set; }
    }
}
