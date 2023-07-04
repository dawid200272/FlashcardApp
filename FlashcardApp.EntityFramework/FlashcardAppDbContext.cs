using FlashcardApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardApp.EntityFramework
{
    public class FlashcardAppDbContext:DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }

        public FlashcardAppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deck>()
                .HasMany(d => d.Cards)
                .WithOne(c => c.Deck);

            base.OnModelCreating(modelBuilder);
        }
    }
}
