using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class GameImageViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
    }
}
