using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(Title), Name = "UQ__Movies__E52A1BB353799211", IsUnique = true)]
    public partial class Movie
    {
        public Movie()
        {
            MovieActors = new HashSet<MovieActor>();
            MovieDirectors = new HashSet<MovieDirector>();
            MovieGenres = new HashSet<MovieGenre>();
            Trailers = new HashSet<Trailer>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [Column("year")]
        [StringLength(4)]
        public string Year { get; set; }
        [Required]
        [Column("image")]
        public byte[] Image { get; set; }
        [Column("rating")]
        public int Rating { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(Rating))]
        [InverseProperty("Movies")]
        public virtual Ratings RatingNavigation { get; set; }
        [InverseProperty(nameof(MovieActor.Movie))]
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        [InverseProperty(nameof(MovieDirector.Movie))]
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
        [InverseProperty(nameof(MovieGenre.Movie))]
        public virtual ICollection<MovieGenre> MovieGenres { get; set; }
        [InverseProperty(nameof(Trailer.Movie))]
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
