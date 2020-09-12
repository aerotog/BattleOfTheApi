# gRPC

The gRPC API uses binary serialization rather than JSON. You'll need a gRPC client like (grpcc)[https://github.com/njpatel/grpcc] to run queries against it.

Install grpcc with: `npm install -g grpcc`

## grpcc notes
For reasons unknown grpcc fails requests with "Stream Removed" if the .Net project is running with https. (Even with the optional --insecure/-i flag) Because of this, the launchSettings.json are intentionally configured to disable https.

Additionally grpcc 1.1.3 which was used to test hangs when trying to open the orders.proto without specifying the 'orders' service. The optional --service/-s flag fixes this.

## grpcc commands
With the project being hosted at 127.0.0.1:5001:

```
grpcc -a 127.0.0.1:5001 -p Protos/users.proto -i
grpcc -a 127.0.0.1:5001 -p Protos/products.proto -i
grpcc -a 127.0.0.1:5001 -p Protos/orders.proto -i -s orders
grpcc -a 127.0.0.1:5001 -p Protos/items.proto -i
```

Google dotnet gRPC notes: https://chromium.googlesource.com/external/github.com/grpc/grpc/+/HEAD/src/csharp/BUILD-INTEGRATION.md