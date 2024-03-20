using Service.Data;
using Stl.Fusion;
using Stl.Fusion.Extensions;
using Stl.Fusion.Server;
using Stl.Rpc;

namespace Server.Infrastructure.ServiceCollection
{
    public static class FusionServices
    {
        public static IServiceCollection AddFusionServices(this IServiceCollection services)
        {
            // Fusion services
            var fusion = services.AddFusion(RpcServiceMode.Server, true);

            var fusionServer = fusion.AddWebServer();


            fusion.AddSandboxedKeyValueStore();
            fusion.AddOperationReprocessor();

            fusion.AddDbKeyValueStore<AppDbContext>();
            fusion.AddEbazarServices();

            return services;
        }
    }
}
