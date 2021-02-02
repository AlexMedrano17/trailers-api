using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("Genre")]
    [Index(nameof(Name), IsUnique = true)]
    public partial class Genre
    {
        public Genre()
        {
            TrailerGenres = new HashSet<TrailerGenre>();
        }

        [Key]
        [Column("ID", TypeName = "INT")]
        public long Id { get; set; }
        [Required]
        [Column("name", TypeName = "VARCHAR (50)")]
        public string Name { get; set; }

        [InverseProperty(nameof(TrailerGenre.Genre))]
        public virtual ICollection<TrailerGenre> TrailerGenres { get; set; }
    }
}
