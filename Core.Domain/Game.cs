using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public enum Genres { Sport, MurderMystery, Farming, Fantasy, Simulation, Trivia, Family, Mind, Dexterity, Shooting, Racing, Travel } // more to come?
        public Genres genre { get; set; }
        public Boolean AdultsOnly { get; set; }
        public enum Category { Kaartspel, Bordspel, Computerspel, Tafelspel}
        public Category category { get; set; }

    }
}
