using System;
using System.ComponentModel.DataAnnotations;

namespace portal.Models
{
    public class CreateReviewViewModel
    {
        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        public string ReviewText { get; set; }
    }
}

