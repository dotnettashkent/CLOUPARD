using Stl.CommandR;
using Shared.Features;
using Shared.Infrastructures;
using Shared.Features.Product;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructures.Extensions;

namespace Server.Controllers.Product
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICommander commander;

        public ProductController(IProductService productService, ICommander commander)
        {
            this.productService = productService;
            this.commander = commander;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await commander.Call(command,cancellationToken);
            return new ObjectResult(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await commander.Call(command, cancellationToken);
            return new ObjectResult(result);
        }


        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var result = await commander.Call(command, cancellationToken);
            return new ObjectResult(result);
        }

        [HttpGet("get/all")]
        public async Task<TableResponse<ProductView>> GetAll([FromQuery] TableOptions options, CancellationToken cancellationToken = default)
        {
            return await productService.GetAll(options, cancellationToken);
        }

        [HttpGet("get")]
        public async Task<ProductView> Get(Guid Id, CancellationToken cancellationToken = default)
        {
            return await productService.Get(Id, cancellationToken);
        }
    }
}
