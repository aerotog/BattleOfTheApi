# gRPC

The gRPC API does not use JSON. You'll need a gRPC client like (grpcc)[https://github.com/njpatel/grpcc] to run queries against it.

Install grpcc with: `npm install -g grpcc`

NOTE: For some reason grpcc fails requests with "Stream Removed" if the .Net project is running with https. Because of this, the launchSettings.json are intentionally configured to disable https.

