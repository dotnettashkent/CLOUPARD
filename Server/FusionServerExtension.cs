using Service.Features;
using Shared.Features;
using Stl.Fusion;

namespace Server
{
    public static class FusionServerExtension
    {
        public static FusionBuilder AddEbazarServices(this FusionBuilder fusion)
        {
            fusion.AddService<IProductService, ProductService>();

            return fusion;
        }
    }
}
