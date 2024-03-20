using Shared.Features.Product;
using Stl.Fusion;
using System.Runtime.Serialization;

namespace Shared.Features
{
    public partial record CreateProductCommand(
        [property: DataMember] Session? Session,
        [property: DataMember] ProductView Entity) : ISessionCommand<ProductView>;

    
    public partial record UpdateProductCommand(
        [property: DataMember] Session? Session,
        [property: DataMember] ProductView Entity) : ISessionCommand<ProductView>;

    public partial record DeleteProductCommand(
        [property: DataMember] Session? Session,
        [property: DataMember] Guid Id) : ISessionCommand<ProductView>;

}
