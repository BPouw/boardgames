using System;
namespace Core.Domain
{
    public class GameNightPlayer
    {
        public GameNight GameNight { get; private set; }
        public int GameNightId { get; set; }
        public Person Person { get; private set; }
        public int PersonId { get; set; }
    }
}

