using Domain;
using Microsoft.EntityFrameworkCore;

namespace HighSchoolLesson.DataAccess
{
	public class LibraryContext : DbContext
	{
		public LibraryContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<Book> Books  { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Visitor> Visitors { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=BorisHome\\Boris;Database=LibraryAdoNet;Trusted_Connection=true;");

		}
	}
}
