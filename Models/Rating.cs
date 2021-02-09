using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(Rating), Name = "UQ__Ratings__560C75949E84EDC4", IsUnique = true)]
    public partial class Ratings
    {
        public Ratings()
        {
            Movies = new HashSet<Movie>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("rating")]
        [StringLength(50)]
        public string Rating { get; set; }

        [InverseProperty(nameof(Movie.RatingNavigation))]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
