using LibrariumAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Models
{
    public class LibraryContext : DbContext
    {

        public DbSet<Books> Books => Set<Books>();
        public DbSet<Readers> Readers => Set<Readers>();
        public DbSet<Librarians> Librarians => Set<Librarians>();
        public DbSet<IssueCards> IssueCards => Set<IssueCards>();
        public DbSet<BookPlace> BookPlaces => Set<BookPlace>();

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            base.OnModelCreating(modelBuilder);
        }
    }
}