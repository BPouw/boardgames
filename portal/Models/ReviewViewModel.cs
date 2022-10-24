using System;
using Core.Domain;
namespace portal.Models
{
    public class ReviewViewModel
    {
        public Person Reviewer { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }
}

