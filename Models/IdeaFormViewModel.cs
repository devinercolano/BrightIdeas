using System;
using System.ComponentModel.DataAnnotations;

namespace Belt.Models{
    public class IdeaFormViewModel{
        
        public int IdeaId {get; set;}
        
        [Display(Name="Idea")]
        [Required]
        [MinLength(2)]
        public string IdeaText {get;set;}
        public int UserId {get; set;}

    }
}