using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Table("Trailer")]
    [Index(nameof(ImgUrl), IsUnique = true)]
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(Url), IsUnique = true)]
    public partial class Trailer
    {
        public Trailer()
        {
            TrailerGenres = new HashSet<TrailerGenre>();
        }

        [Key]
        [Required]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("title", TypeName = "VARCHAR (100)")]
        public string Title { get; set; }
        [Required]
        [Column("year", TypeName = "CHAR (4)")]
        public string Year { get; set; }
        [Required]
        [Column("url", TypeName = "VARCHAR")]
        public string Url { get; set; }
        [Required]
        [Column("img_url", TypeName = "VARCHAR")]
        public string ImgUrl { get; set; }
        [Required]
        [Column("shedule_date", TypeName = "DATETIME")]
        public DateTime SheduleDate { get; set; }

        [InverseProperty(nameof(TrailerGenre.Trailer))]
        public virtual ICollection<TrailerGenre> TrailerGenres { get; set; }
    }
}
