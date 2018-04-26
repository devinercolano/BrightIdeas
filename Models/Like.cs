
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Belt.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int LikeTab { get; set; }
        public int UserId { get; set; }
        public int IdeaId {get; set;}
        public User User { get; set; }
        public Idea Idea { get; set; }
        
    }
}