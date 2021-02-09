using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("MovieDirector")]
    public partial class MovieDirector
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("movie_id")]
        public int MovieId { get; set; }
        [Column("director_id")]
        public int DirectorId { get; set; }

        [ForeignKey(nameof(DirectorId))]
        [InverseProperty("MovieDirectors")]
        public virtual Director Director { get; set; }
        [ForeignKey(nameof(MovieId))]
        [InverseProperty("MovieDirectors")]
        public virtual Movie Movie { get; set; }
    }
}
