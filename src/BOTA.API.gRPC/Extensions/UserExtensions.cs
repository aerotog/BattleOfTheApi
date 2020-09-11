namespace BOTA.API.gRPC.Extensions
{
    public static class UserExtensions
    {
        public static User ToProto(this BOTA.Core.Models.User user)
        {
            return new User
            {
                Id = user.Id,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

        }
    }
}
