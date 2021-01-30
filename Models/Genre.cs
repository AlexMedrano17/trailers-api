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
            Trailers = new HashSet<Trailer>();
        }

        [Key]
        [Column("ID", TypeName = "INT")]
        public long Id { get; set; }
        [Required]
        [Column("name", TypeName = "VARCHAR (50)")]
        public string Name { get; set; }

        [InverseProperty(nameof(Trailer.GenreNavigation))]
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
