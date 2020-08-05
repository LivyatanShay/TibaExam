using Microsoft.EntityFrameworkCore;

namespace TibaExam.Models
{
    public partial class FavoritesContext : DbContext
    {
        public FavoritesContext(DbContextOptions<FavoritesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Favorite> Favorites { get; set; }
    }
}
