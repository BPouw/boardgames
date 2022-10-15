using System;
using Core.Domain;

namespace portal.Models
{
    public class GameListViewModel
    {
        public Game Game { get; set; }
        public int GameID { get; set; }

        public GameNight GameNight { get; set; }
        public int GameNightID { get; set; }
    }
}

