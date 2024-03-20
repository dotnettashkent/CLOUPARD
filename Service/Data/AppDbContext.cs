
using EF.Audit.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features;
using Stl.Fusion.Authentication.Services;
using Stl.Fusion.EntityFramework;
using Stl.Fusion.EntityFramework.Operations;
using Stl.Fusion.Extensions.Services;

namespace Service.Data
{
    public partial class AppDbContext : DbContextBase
    {
        public IServiceScopeFactory _serviceScopeFactory;

        [ActivatorUtilitiesConstructor]
        public AppDbContext(DbContextOptions<AppDbContext> options,
            IServiceScopeFactory serviceScopeFactory) : base(options)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        // Stl.Fusion.EntityFramework tables
        public DbSet<DbUser<string>> StlFusionUsers { get; protected set; } = null!;
        public DbSet<DbUserIdentity<string>> UserIdentities { get; protected set; } = null!;
        public DbSet<DbSessionInfo<string>> Sessions { get; protected set; } = null!;
        public DbSet<DbKeyValue> KeyValues { get; protected set; } = null!;
        public DbSet<DbOperation> Operations { get; protected set; } = null!;

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChanges();

        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Detached || x.State == EntityState.Unchanged || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }

        }
    }
}
