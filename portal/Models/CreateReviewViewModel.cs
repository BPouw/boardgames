using System;
using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class CreateReviewViewModel
    {
        [Required]
        public int Rating { get; set; }

        [Required]
        [StringLength(200, ErrorMessage ="A review can only be 200 characters long")]
        public string ReviewText { get; set; }
    }
}

