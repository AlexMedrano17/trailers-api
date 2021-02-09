﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(Url), Name = "UQ__Trailers__DD7784179FA620A8", IsUnique = true)]
    public partial class Trailer
    {
        public Trailer()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("url")]
        public string Url { get; set; }
        [Column("movie_id")]
        public int MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        [InverseProperty("Trailers")]
        public virtual Movie Movie { get; set; }
        [InverseProperty(nameof(Comment.Trailer))]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
