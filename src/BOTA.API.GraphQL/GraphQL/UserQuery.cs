﻿using System.Linq;
using BOTA.Core.Repository;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

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
                    return shopContext
                        .Users
                        .Include(x => x.Orders)
                        .ThenInclude(x => x.Items)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefault(i => i.Id == id);
                });

            Field<ListGraphType<UserType>>(
                "Users",
                resolve: context => shopContext.Users);
        }
    }
}
