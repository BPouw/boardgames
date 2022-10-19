using System;
using Core.Domain;

namespace portal.Models
{
    public class GameNightGameViewModel
    {
        public Game Game { get; set; }
        public int GameID { get; set; }

        public GameNight GameNight { get; set; }
        public int GameNightId { get; set; }
    }
}

