using System.Linq;
using System.Threading.Tasks;
using BOTA.API.gRPC.Extensions;
using BOTA.Core.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace BOTA.API.gRPC.Services
{
    public class ProductsService : ProductsProto.ProductsProtoBase
    {
        private readonly ShopContext _shopContext;

        public ProductsService(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public override async Task<Products> GetProducts(Empty _, ServerCallContext context)
        {
            var products = await _shopContext.Products.ToListAsync();
            var protoProducts = products.Select(x => x.ToProto());

            var response = new Products();
            response.Products_.Add(protoProducts);
            return response;
        }

        public override async Task<Product> GetProduct(ProductId productId, ServerCallContext context)
        {
            var product = await _shopContext.Products.FindAsync(productId.Id);

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No product found with Id {productId.Id}"));
            }

            return product.ToProto();
        }

        public override async Task<Product> UpdateProduct(Product product, ServerCallContext context)
        {
            var productEntity = product.ToEntity();
            _shopContext.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _shopContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"No product found with Id {product.Id}"));
                }

                throw;
            }

            return productEntity.ToProto();
        }

        public override async Task<Product> AddProduct(Product product, ServerCallContext context)
        {
            var productEntity = product.ToEntity();
            await _shopContext.Products.AddAsync(productEntity);
            await _shopContext.SaveChangesAsync();

            return productEntity.ToProto();
        }

        public override async Task<Product> DeleteProduct(ProductId productId, ServerCallContext context)
        {
            var product = await _shopContext.Products.FindAsync(productId.Id);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No product found with Id {product.Id}"));
            }

            _shopContext.Products.Remove(product);
            await _shopContext.SaveChangesAsync();

            return product.ToProto();
        }

        private bool ProductExists(int id)
        {
            return _shopContext.Products.Any(e => e.Id == id);
        }
    }
}
