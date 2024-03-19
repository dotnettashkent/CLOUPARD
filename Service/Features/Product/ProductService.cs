using Dashboard.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Features;
using Shared.Features.Product;
using Shared.Infrastructures;
using Shared.Infrastructures.Extensions;
using Stl.Async;
using Stl.Fusion;
using Stl.Fusion.Authentication;
using Stl.Fusion.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Reactive;

namespace Service.Features
{
    public class ProductService : IProductService
    {
        #region Initialize
        private readonly DbHub<AppDbContext> dbHub;
        private readonly IConfiguration configuration;

        public ProductService(DbHub<AppDbContext> dbHub,  IConfiguration configuration)
        {
            this.dbHub = dbHub;
            this.configuration = configuration;
        }
        #endregion
        #region Queries
        public async virtual Task<TableResponse<ProductView>> GetAll(TableOptions options, CancellationToken cancellationToken = default)
        {
            await Invalidate();
            var dbContext = dbHub.CreateDbContext();
            await using var _ = dbContext.ConfigureAwait(false);
            var product = from s in dbContext.Products select s;
            if (!String.IsNullOrEmpty(options.search))
            {
                product = product.Where(s =>
                         s.Name != null && s.Name.Contains(options.search)
                         || s.Description.Contains(options.search)
                );
            }

            Sorting(ref product, options);


            var count = await product.AsNoTracking().CountAsync();
            var items = await product.AsNoTracking().Paginate(options).ToListAsync();
            decimal totalPage = (decimal)count / (decimal)options.page_size;
            return new TableResponse<ProductView>() { Items = items.MapToViewList(), TotalItems = count, AllPage = (int)Math.Ceiling(totalPage), CurrentPage = options.page };
        }
        public async virtual Task<ProductView> GetById(Guid Id, CancellationToken cancellationToken = default)
        {
            var dbContext = dbHub.CreateDbContext();
            await using var _ = dbContext.ConfigureAwait(false);
            var product = await dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == Id);

            return product == null ? throw new CustomException("ProductEntity Not Found") : product.MapToView();
        }

        public async virtual Task<ProductView> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            var dbContext = dbHub.CreateDbContext();
            await using var _ = dbContext.ConfigureAwait(false);
            var product = await dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == Id);

            return product == null ? throw new ValidationException("ProductEntity Not Found") : product.MapToView();
        }
        #endregion

        #region Mutations
        public async virtual Task Create(CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            if (Computed.IsInvalidating())
            {
                _ = await Invalidate();
                return;
            }
            command.Session.IsDefault();
            
            await using var dbContext = await dbHub.CreateCommandDbContext(cancellationToken);
            ProductEntity entity = new ProductEntity();
            Reattach(entity, command.Entity, dbContext);
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async virtual Task Delete(DeleteProductCommand command, CancellationToken cancellationToken = default)
        {
            if (Computed.IsInvalidating())
            {
                _ = await Invalidate();
                return;
            }
            command.Session.IsDefault();
            await using var dbContext = await dbHub.CreateCommandDbContext(cancellationToken);
            var entity = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (entity == null)
                throw new CustomException("ProductEntity Not Found");
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
        public async virtual Task Update(UpdateProductCommand command, CancellationToken cancellationToken = default)
        {
           
            if (Computed.IsInvalidating())
            {
                _ = await Invalidate();
                return;
            }
            command.Session.IsDefault();
            await using var dbContext = await dbHub.CreateCommandDbContext(cancellationToken);
            var entity = await dbContext.Products
                .FirstOrDefaultAsync(x => x.Id == command.Entity!.Id);

            if (entity == null)
                throw new CustomException("ProductEntity Not Found");
          

            Reattach(entity, command.Entity, dbContext);
            await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Helpers
        //[ComputeMethod]
        public virtual Task<Unit> Invalidate() => TaskExt.UnitTask;

        private ProductEntity Reattach(ProductEntity entity, ProductView view, AppDbContext dbContext)
        {
            ProductMapper.From(view, entity);
            return entity;

        }
        
        private void Sorting(ref IQueryable<ProductEntity> offering, TableOptions options) => offering = options.sort_label switch
        {
            "Name" => offering.Ordering(options, o => o.Name),
            "Description" => offering.Ordering(options, o => o.Description)
        };
        #endregion

    }
}
