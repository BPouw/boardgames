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
        public Genre genre { get; set; }
        public Boolean AdultsOnly { get; set; }
        public Category category { get; set; }
        public GameImage GameImage { get; set; }
        public int? GameImageId { get; set; }
        public ICollection<GameNight> GameNights { get; set;}

    }
}
