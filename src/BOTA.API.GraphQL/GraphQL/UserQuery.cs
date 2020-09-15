using System.Linq;
using BOTA.Core.Repository;
using GraphQL;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery(ShopContext shopContext)
        {
            Field<UserType>(
                "User",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType>
                    {
                        Name = "id", Description = "Id of the User"
                    }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    //var user = shopContext.Users.FirstOrDefault();
                    var user = shopContext.Users.FirstOrDefault(i => i.Id == id);
                    return user;
                });

            Field<ListGraphType<UserType>>(
                "Users",
                resolve: context =>
                {
                    var users = shopContext.Users;
                    return users;
                });
        }
    }
}
