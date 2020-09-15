using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BOTA.API.GraphQL.Dtos;
using BOTA.API.GraphQL.GraphQL;
using BOTA.Core.Repository;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BOTA.API.GraphQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphQLController : ControllerBase
    {
        private readonly ILogger<GraphQLController> _logger;
        private readonly ShopContext _dbContext;

        public GraphQLController(ILogger<GraphQLController> logger, ShopContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(GraphQLQuery query)
        //public async Task<IActionResult> PostAsync(System.Text.Json.JsonElement rawQuery)
        {
            //string rawJson = rawQuery.ToString();
            //var query = JsonConvert.DeserializeObject<GraphQLQuery>(rawJson);
            //query = System.Text.Json.JsonSerializer.Deserialize<GraphQLQuery>(rawJson);
            //Console.WriteLine(query.NamedQuery);
            //Console.WriteLine(query.OperationName);
            //Console.WriteLine(query.Query);
            //Console.WriteLine(query.Variables);

            //Dotnet 3.1 deserializes Dictionary<string,object> types into JsonElement values. We need the values as primitive types 
            //TODO Create custom JSON converter
            query.Variables = query.Variables.ToDictionary(x => x.Key,
                x =>
                {
                    if (x.Value is JsonElement element)
                    {
                        //TODO Handle more types and safely
                        switch (element.ValueKind)
                        {
                            case JsonValueKind.Number:
                                return element.GetInt32();
                        }
                    }

                    return x.Value;
                });


            Inputs inputs = null;

            if (query.Variables != null)
            {
                //var dictionary =
                //    System.Text
                //        .Json
                //        .JsonSerializer
                //        .Deserialize<Dictionary<string, object>>(
                //            query.Variables);
                inputs = new Inputs(query.Variables.ToDictionary(x => x.Key, x => (object)x.Value));
                //inputs = new Inputs(query.Variables);
            }


            var schema = new Schema
            {
                Query = new UserQuery(_dbContext)
            };

            var result = await new DocumentExecuter().ExecuteAsync(options =>
            {
                options.Schema = schema;
                options.Query = query.Query;
                options.OperationName = query.OperationName;
                options.Inputs = inputs;
                //options.Inputs = new Inputs(new Dictionary<string, object> {{"id", 1}});
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result);
        }
    }
}
