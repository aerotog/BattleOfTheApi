# gRPC

The gRPC API does not use JSON. You'll need a gRPC client like (grpcc)[https://github.com/njpatel/grpcc] to run queries against it.

Install grpcc with: `npm install -g grpcc`

NOTE: For some reason grpcc fails requests with "Stream Removed" if the .Net project is running with https. Because of this, the launchSettings.json are intentionally configured to disable https.

grpcc -a 127.0.0.1:5001 -p users.proto -i

Google dotnet gRPC notes: https://chromium.googlesource.com/external/github.com/grpc/grpc/+/HEAD/src/csharp/BUILD-INTEGRATION.md