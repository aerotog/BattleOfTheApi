using BOTA.API.REST.JsonApi.Models;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using JsonApiDotNetCore.Configuration;
using Microsoft.Extensions.Logging;

namespace BOTA.API.REST.JsonApi.Controllers
{
    public class UsersController : JsonApiController<User>
    {
        public UsersController(
            IJsonApiOptions options,
            ILoggerFactory loggerFactory,
            IResourceService<User> resourceService)
            : base(options, loggerFactory, resourceService)
        { }
    }
}
