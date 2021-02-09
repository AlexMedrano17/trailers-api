using System;
using System.Collections.Generic;

namespace trailers_api.Models.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<int> Genres { get; set; }
        public string Year { get; set; }
        public List<int> Actors { get; set; }
        public List<int> Directors { get; set; }
        public List<int> Ratings { get; set; }
        public byte[] Image { get; set; }
        public List<string> Trailers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}