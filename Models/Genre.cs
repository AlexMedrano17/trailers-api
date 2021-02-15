﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(Genre1), Name = "UQ__Genres__8660A0C851EA039F", IsUnique = true)]
    public partial class Genre
    {
        public Genre()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("genre")]
        [StringLength(50)]
        public string Genre1 { get; set; }

        [InverseProperty(nameof(MovieGenre.Genre))]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    }
}