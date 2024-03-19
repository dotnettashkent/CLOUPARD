using Microsoft.EntityFrameworkCore;
using Stl.Fusion.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features;

namespace Dashboard.Services;
public partial class AppDbContext : DbContextBase
{
    public IServiceScopeFactory _serviceScopeFactory;
    public AppDbContext()
    {

    }

    [ActivatorUtilitiesConstructor]
    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public virtual DbSet<ProductEntity> Products { get; set; }
}