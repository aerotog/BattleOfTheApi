using BOTA.API.REST.JsonApi.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using JsonApiDotNetCore.Configuration;
using Microsoft.Extensions.Logging;

namespace BOTA.API.REST.JsonApi.Controllers
{
    public class ProductsController : JsonApiController<Product>
    {
        public ProductsController(
            IJsonApiOptions options,
            ILoggerFactory loggerFactory,
            IResourceService<Product> resourceService)
            : base(options, loggerFactory, resourceService)
        { }
    }
}
