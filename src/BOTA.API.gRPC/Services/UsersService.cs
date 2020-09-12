using System.Linq;
using System.Threading.Tasks;
using BOTA.API.gRPC.Extensions;
using BOTA.Core.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace BOTA.API.gRPC.Services
{
    public class UsersService : UsersProto.UsersProtoBase
    {
        private readonly ShopContext _shopContext;

        public UsersService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public override async Task<User> GetUser(UserId userId, ServerCallContext context)
        {
            var user = await _shopContext
                .Users
                .Include(x => x.Orders)
                .Where(x => x.Id == userId.Id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No user found with Id {userId.Id}"));
            }

            return user.ToProto();
        }

        public override async Task<Users> GetUsers(Empty _, ServerCallContext context)
        {
            var users = await _shopContext.Users.ToListAsync();
            var protoUsers = users.Select(x => new User
            {
                City = x.City,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName
            });

            var response = new Users();
            response.Users_.Add(protoUsers);
            return response;
        }

        public override async Task GetUsersStream(
            Empty _,
            IServerStreamWriter<User> streamWriter,
            ServerCallContext context)
        {
            var users = await _shopContext.Users.ToListAsync();
            var protoUsers = users.Select(x => new User
            {
                City = x.City,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName
            });

            foreach (var protoUser in protoUsers)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                await streamWriter.WriteAsync(protoUser);
            }
        }

        public override async Task<User> UpdateUser(User user, ServerCallContext context)
        {
            var userEntity = user.ToEntity();
            _shopContext.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_shopContext.Users.Any(e => e.Id == user.Id) == false)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"No user found with Id {user.Id}"));
                }

                throw;
            }

            return userEntity.ToProto();
        }

        public override async Task<User> AddUser(User user, ServerCallContext context)
        {
            var entity = user.ToEntity();

            await _shopContext.Users.AddAsync(entity);
            await _shopContext.SaveChangesAsync();

            return entity.ToProto();
        }

        public override async Task<User> DeleteUser(UserId userId, ServerCallContext context)
        {
            var user = await _shopContext.Users.FindAsync(userId.Id);
            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No user found with Id {userId.Id}"));
            }

            _shopContext.Users.Remove(user);
            await _shopContext.SaveChangesAsync();

            return user.ToProto();
        }
    }
}
