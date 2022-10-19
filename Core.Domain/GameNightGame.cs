using System;
namespace Core.Domain
{
    public class GameNightGame
    {
        public int GameId { get; set; }
        public Game Game { get; private set; }
        public int GameNightId { get; set; }
        public GameNight GameNight { get; private set; }
    }
}

