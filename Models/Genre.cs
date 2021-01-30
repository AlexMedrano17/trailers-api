using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace trailers_api.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Trailers = new HashSet<Trailer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
    }
}
