using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace A7
{
    /// <summary>
    /// The class that represents the database context.
    /// </summary>
    internal class LibraryContext : DbContext
    {
        /// <summary>
        /// Method that initializes the database context.
        /// </summary>
        public LibraryContext() : base("LibraryDBConnection")
        {
            this.Configuration.LazyLoadingEnabled = true; // Lazy loading is enabled
        }

        public DbSet<LibraryItem> LibraryItems { get; set; } // DbSet for LibraryItem, this will be used to interact with the LibraryItems table.
        public DbSet<Loan> Loans { get; set; } // DbSet for Loan, this will be used to interact with the Loans table.

        /// <summary>
        /// Method that configures the database context.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuring the LibraryItem table
            modelBuilder.Entity<LibraryItem>()
                .Map<Book>(m => m.Requires("Discriminator").HasValue("Book"))
                .Map<Film>(m => m.Requires("Discriminator").HasValue("Film"))
                .Map<Article>(m => m.Requires("Discriminator").HasValue("Article"))
                .Map<NewsPaper>(m => m.Requires("Discriminator").HasValue("Newspaper"));
            // Configuring the Loan table
            modelBuilder.Entity<Loan>()
                .HasRequired(l => l.LibraryItem)
                .WithMany()
                .HasForeignKey(l => l.LibraryItemId);
        }
    }
}
