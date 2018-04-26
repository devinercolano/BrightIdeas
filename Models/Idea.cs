
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Belt.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId { get; set; }
        public string IdeaText { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Like> Likes { get; set; }

    }
}