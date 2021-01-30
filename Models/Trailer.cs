using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(ImgUrl), IsUnique = true)]
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(Url), IsUnique = true)]
    public partial class Trailer
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [Column("title", TypeName = "VARCHAR (100)")]
        public string Title { get; set; }
        [Column("genre", TypeName = "INT")]
        public long Genre { get; set; }
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
        public byte[] SheduleDate { get; set; }

        [ForeignKey(nameof(Genre))]
        [InverseProperty("Trailers")]
        public virtual Genre GenreNavigation { get; set; }
    }
}
