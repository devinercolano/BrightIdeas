using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Belt.Models
{
    public class User: BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Like> Likes { get; set; }
        public List<Idea> Ideas {get; set;}
    }
}