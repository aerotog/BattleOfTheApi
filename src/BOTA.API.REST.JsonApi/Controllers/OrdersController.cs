using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using JsonApiDotNetCore.Configuration;
using Microsoft.Extensions.Logging;
using BOTA.API.REST.JsonApi.Models;

namespace BOTA.API.REST.JsonApi.Controllers
{
    public class OrdersController : JsonApiController<Order>
    {
        public OrdersController(
            IJsonApiOptions options,
            ILoggerFactory loggerFactory,
            IResourceService<Order> resourceService)
            : base(options, loggerFactory, resourceService)
        { }
    }
}
