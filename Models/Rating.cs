using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        [DisplayName("Your Name")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20")]
        public string ViewerName { get; set; }
        [DisplayName("Description about your Rating")]
        [Required(ErrorMessage = "You have to provide a valid Description")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(100, ErrorMessage = "Name mustn't exceed 100")]
        public string RatingDescription { get; set; }
        [DisplayName("Your Rating")]
        [Required(ErrorMessage = "You have to provide a valid Rating")]
        [Range(0, 10, ErrorMessage = "the Range is from 0 to 10")]
        public decimal RatingCount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Add The Movie please")]

        [DisplayName("Movie Name")]
        public int MovieId { get; set; }
        [ValidateNever]
        public Movie movie { get; set; }

    }
}
