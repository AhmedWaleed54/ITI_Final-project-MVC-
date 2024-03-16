using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Project.Models
{
    public class Actor
    {
        public int id { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name must have more than 2 charcters")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20")]
        public string name { get; set; }

        [DisplayName("Birth of date")]
        [Required(ErrorMessage = "You have to provide a valid name")]
        public DateTime birthDate { get; set; }


    }
}
