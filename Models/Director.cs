using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace trailers_api.Models
{
    [Index(nameof(Name), Name = "UQ__Director__72E12F1B52C764CF", IsUnique = true)]
    public partial class Director
    {
        public Director()
        {
            MovieDirectors = new HashSet<MovieDirector>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [InverseProperty(nameof(MovieDirector.Director))]
        public virtual ICollection<MovieDirector> MovieDirectors { get; set; }
    }
}
