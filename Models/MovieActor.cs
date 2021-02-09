using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("MovieActor")]
    public partial class MovieActor
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("movie_id")]
        public int MovieId { get; set; }
        [Column("actor_id")]
        public int ActorId { get; set; }

        [ForeignKey(nameof(ActorId))]
        [InverseProperty("MovieActors")]
        public virtual Actor Actor { get; set; }
        [ForeignKey(nameof(MovieId))]
        [InverseProperty("MovieActors")]
        public virtual Movie Movie { get; set; }
    }
}
