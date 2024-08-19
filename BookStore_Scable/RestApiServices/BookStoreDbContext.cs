using Microsoft.EntityFrameworkCore;
using RestApiServices.Models;

namespace RestApiServices
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsetting.json");
            IConfiguration configuration = configurationBuilder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookStoreDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.UserId);


            modelBuilder.Entity<Cart>()
                .HasMany(c => c.Books)
                .WithMany(c => c.Carts)
                .UsingEntity("CartBooks");

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Books)
                .WithMany(e => e.Authors)
                .UsingEntity("BooksAuthor");


        }
    }
}
