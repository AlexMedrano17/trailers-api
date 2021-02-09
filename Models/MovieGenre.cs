using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("MovieGenre")]
    public partial class MovieGenre
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("movie_id")]
        public int MovieId { get; set; }
        [Column("genre_id")]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [InverseProperty("MovieGenres")]
        public virtual Genre Genre { get; set; }
        [ForeignKey(nameof(MovieId))]
        [InverseProperty("MovieGenres")]
        public virtual Movie Movie { get; set; }
    }
}
