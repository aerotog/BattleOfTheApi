using System;
using System.Linq;

namespace BOTA.API.gRPC.Extensions
{
    public static class OrderExtensions
    {
        public static Order ToProto(this Core.Models.Order order)
        {
            var proto = new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                Date = order.Date.ToString("O")
            };

            if (order.Items != null)
            {
                var itemProtos = order.Items?.Select(x => x.ToProto());
                proto.Items.Add(itemProtos);
            }

            return proto;
        }

        public static Core.Models.Order ToEntity(this Order order)
        {
            var entity = new Core.Models.Order
            {
                Id = order.Id,
                UserId = order.UserId,
                Date = DateTime.Parse(order.Date)
            };

            if (order.Items != null)
            {
                entity.Items = order.Items.Select(x => x.ToEntity()).ToList();
            }


            return entity;
        }
    }
}
