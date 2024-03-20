using Shared.Features.Product;
using Shared.Infrastructures;
using Shared.Infrastructures.Extensions;
using Stl.Async;
using Stl.CommandR.Configuration;
using Stl.Fusion;
using System.Reactive;

namespace Shared.Features
{
    public interface IProductService : IComputeService
    {
        /// <summary>
        /// it's a memory cache 
        /// </summary>
        /// <param name="[ComputeMethod]"></param>
        /// <returns>project ready to production and remove //comment</returns>
        
        //[ComputeMethod]
        Task<TableResponse<ProductView>> GetAll(TableOptions options, CancellationToken cancellationToken = default);


        /// <summary>
        /// it's a memory cache 
        /// </summary>
        /// <param name="[ComputeMethod]"></param>
        /// <returns>project ready to production and remove //comment</returns>
       
        //[ComputeMethod]
        Task<ProductView> Get(Guid Id, CancellationToken cancellationToken = default);


        [CommandHandler]
        Task Create(CreateProductCommand command, CancellationToken cancellationToken = default);
        [CommandHandler]
        Task Delete(DeleteProductCommand command, CancellationToken cancellationToken = default);
        [CommandHandler]
        Task Update(UpdateProductCommand command, CancellationToken cancellationToken = default);
        Task<Unit> Invalidate() { return TaskExt.UnitTask; }
    }
}
