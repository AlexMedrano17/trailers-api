using System;
using System.Collections.Generic;

namespace trailers_api.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Genres { get; set; }
        public string Year { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Directors { get; set; }
        public string Rating { get; set; }
        public string Image { get; set; }
        public List<string> Trailers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}