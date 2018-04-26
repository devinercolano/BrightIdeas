using System.ComponentModel.DataAnnotations;

namespace Belt.Models
{
    public class RegistrationViewModel
    {
        [Display(Name ="First Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage="First Name can only contain letters")]
        [MinLength(2)]
        public string FirstName { get; set; }
        
        [Display(Name ="Last Name")]
        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage="First Name can only contain letters")]
        [MinLength(2)]
        public string LastName { get; set; }

        [Display(Name ="Email Address")]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [Required]
        [MinLength(8)]
        [RegularExpression("((?=.*[0-9])(?=.*[a-zA-Z])(?=.*[@#$%]).{8,})", ErrorMessage="Password must contain 1 number, 1 letter, and a special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirmation must match.")]
        [DataType(DataType.Password)]
        public string Confirmation { get; set; }
    }
}