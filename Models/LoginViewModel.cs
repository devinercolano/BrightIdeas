
using System.ComponentModel.DataAnnotations;

namespace Belt.Models
{
    public class LoginViewModel
    {
        [Display(Name="Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string logEmail {get;set;}

        [Display(Name="Password")]
        [Required]
        [DataType(DataType.Password)]
        public string logPassword {get;set;}
    }
}