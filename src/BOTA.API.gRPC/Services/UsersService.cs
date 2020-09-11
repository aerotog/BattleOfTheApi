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
        private readonly ShopContext _context;

        public UsersService(ShopContext context)
        {
            _context = context;
        }

        public override async Task<User> GetUsers(
            GetUserRequest request, 
            ServerCallContext context)
        {
            var users = await _context.Users.ToListAsync();
            var protoUsers = users.Select(x => new User
            {
                City = x.City,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName
            });

            return protoUsers.First();
            //var foo = new Users()
            //{
            //    Users_ = protoUsers;
            //};
            //return new Users();
            //return await _context.Users.ToListAsync();
        }

        public override async Task<User> GetUser(
            GetUserRequest request,
            ServerCallContext context)
        {
            var user = await _context
                .Users
                .Include(x => x.Orders)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No user found with Id {request.Id}"));
            }

            return user.ToProto();
        }

        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return null;
        //        //return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return null;
        //            //return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return null;
        //    //return NoContent();
        //}

        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return null;
        //    //return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return null;
        //        //return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
