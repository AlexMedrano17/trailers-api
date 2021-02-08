using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("Trailer_genre")]
    public partial class TrailerGenre
    {
        [Key]
        [Column("ID", TypeName = "INT")]
        public long Id { get; set; }
        [Column("trailer_id", TypeName = "INT")]
        public long? TrailerId { get; set; }
        [Column("genre_id", TypeName = "INT")]
        public long? GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        [InverseProperty("TrailerGenres")]
        public virtual Genre Genre { get; set; }
        [ForeignKey(nameof(TrailerId))]
        [InverseProperty("TrailerGenres")]
        public virtual Trailer Trailer { get; set; }
    }
}
