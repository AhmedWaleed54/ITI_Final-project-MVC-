using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Project.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [DisplayName("Name of Movie")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20")]
        public string MovieTitle { get; set; }
        [DisplayName("Description of Movie")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(100, ErrorMessage = "Name mustn't exceed 100")]
        public string MovieDescription { get; set; }
        [DisplayName("Type of Movie")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20")]
        public string MovieType { get; set; }

        [Required(ErrorMessage ="Please Add a valid URL")]
        public string MovieUrl { get; set;}
        [ValidateNever]
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [ValidateNever]
        public List<Rating>rating { get; set; }



    }
}
