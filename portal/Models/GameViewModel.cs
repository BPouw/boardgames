using Core.Domain;

namespace portal.Models
{
    public class GameViewModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public Genre genre { get; set; }
        public Boolean AdultsOnly { get; set; }
        public Category category { get; set; }
        public GameImage GameImage { get; set; }

        public string Picture { get; set; }

        public string PictureFormat { get; set; }
    }
}
