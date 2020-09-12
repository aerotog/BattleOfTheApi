using System.Linq;
using System.Threading.Tasks;
using BOTA.API.gRPC.Extensions;
using BOTA.Core.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace BOTA.API.gRPC.Services
{
    public class OrdersService : OrdersProto.OrdersProtoBase
    {
        private readonly ShopContext _shopContext;

        public OrdersService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public override async Task<Orders> GetOrders(Empty _, ServerCallContext context)
        {
            var orders = await _shopContext.Orders.ToListAsync();

            var orderProtos = orders.Select(x => x.ToProto()).ToList();

            var response = new Orders();
            response.Orders_.Add(orderProtos);

            return response;
        }

        public override async Task<Order> GetOrder(OrderId orderId, ServerCallContext context)
        {
            var order = await _shopContext
                .Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == orderId.Id)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No order found with Id {orderId.Id}"));
            }

            return order.ToProto();
        }

        public override async Task<Order> UpdateOrder(Order order, ServerCallContext context)
        {
            var entity = order.ToEntity();
            _shopContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"No order found with Id {order.Id}"));
                }
                else
                {
                    throw;
                }
            }

            return entity.ToProto();
        }

        public override async Task<Order> AddOrder(Order order, ServerCallContext context)
        {
            var entity = order.ToEntity();
            await _shopContext.Orders.AddAsync(entity);
            await _shopContext.SaveChangesAsync();

            return entity.ToProto();
        }

        public override async Task<Order> DeleteOrder(OrderId orderId, ServerCallContext context)
        {
            var order = await _shopContext.Orders.FindAsync(orderId.Id);
            if (order == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No order found with Id {orderId.Id}"));
            }

            _shopContext.Orders.Remove(order);
            await _shopContext.SaveChangesAsync();

            return order.ToProto();
        }

        private bool OrderExists(int id)
        {
            return _shopContext.Orders.Any(e => e.Id == id);
        }
    }
}
