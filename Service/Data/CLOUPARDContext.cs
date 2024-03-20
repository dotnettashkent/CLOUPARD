using Microsoft.EntityFrameworkCore;
using Shared.Features;
using Shared.Infrastructures;
using Stl.Fusion.Authentication;

namespace Service.Data
{
    public partial class AppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
    }
}
