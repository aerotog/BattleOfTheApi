namespace BOTA.API.gRPC.Extensions
{
    public static class UserExtensions
    {
        public static User ToProto(this Core.Models.User user)
        {
            return new User
            {
                Id = user.Id,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static Core.Models.User ToEntity(this User user)
        {
            return new Core.Models.User
            {
                Id = user.Id,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
