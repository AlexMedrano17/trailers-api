﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using trailers_api.Models;

#nullable disable

namespace trailers_api.Data
{
    public partial class trailersContext : DbContext
    {
        public trailersContext()
        {
            this.Database.EnsureCreated();
        }

        public trailersContext(DbContextOptions<trailersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Trailer> Trailers { get; set; }
        public virtual DbSet<TrailerGenre> TrailerGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trailer>(entity =>
            {
                entity.Property(e => e.SheduleDate).HasDefaultValueSql("datetime('now', 'localtime')");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
