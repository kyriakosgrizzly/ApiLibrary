﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)] 
        public string Description { get; set; } = string.Empty;

        [StringLength(250)]
        public string Image { get; set; } = string.Empty;

        [Range(0, 10)] 
        public double Rating { get; set; } 

        [Required]
        [Column(TypeName = "date")]
        public DateTime PublishmentDate { get; set; } // Changed from DateOnly to DateTime
        [Required]
        public bool IsTaken { get; set; }
        [Required]
        public List<Author> AuthorList { get; set; }
    }
}
