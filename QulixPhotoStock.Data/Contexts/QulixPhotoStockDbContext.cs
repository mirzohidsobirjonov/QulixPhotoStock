
using Microsoft.EntityFrameworkCore;
using QulixPhotoStock.Domain.Entities.Authors;
using QulixPhotoStock.Domain.Entities.Photos;
using QulixPhotoStock.Domain.Entities.Texts;

namespace QulixPhotoStock.Data.Contexts
{
    public class QulixPhotoStockDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Text> Texts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Database=QulixPhotoStockDb;Username=postgres;Password=123";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
