using BOTA.Core.Models;
using GraphQL.Types;

namespace BOTA.API.GraphQL.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Name = "User";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("ID of the User");
            Field(x => x.FirstName).Description("First name of the User");
            Field(x => x.LastName).Description("Last name of the User");
            Field(x => x.City).Description("Cit of the User");
        }

    }
}
