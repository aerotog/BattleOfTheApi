using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BOTA.API.GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var input = new GraphQLQuery
            //{
            //    Query = "query UserQuery($id:Int!){  user(id: $id){    Id    City	}	}",
            //    Variables = new Dictionary<string, int> {{"id", 1}},
            //    OperationName = "UserQuery"
            //};

            //var rawJson = System.Text.Json.JsonSerializer.Serialize(input);

            //var query = JsonConvert.DeserializeObject<GraphQLQuery>(rawJson);
            //query = System.Text.Json.JsonSerializer.Deserialize<GraphQLQuery>(rawJson);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
