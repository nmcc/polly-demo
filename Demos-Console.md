# Demo guide - Console application

All demos require the Web API to be running.

> **NOTE:** Request tracing will be enabled for demo purposes.

```bash
# Console 1
$ cd web-api
$ dotnet run --launch-profile Tracing
```

## Running Retry demo

```bash
# Console 2
$ cd client-cli

# Running the non-resilient client. Hit Ctrl-C to stop the applicaton
$ dotnet run retry

# Running the resilient resilient. Hit Ctrl-C to stop the applicaton
$ dotnet run retry2
```

## Running Fallback demo

```bash
# Console 2
$ cd client-cli

# Run the client without Fallback Policy
$ dotnet run fallback

# Run the client with Fallback Policy
$ dotnet run fallback2
```

## Running Circuit Breaker demo

```bash
# Console 2 
$ cd client-cli

# Run the client without Circuit Breaker Policy
$ dotnet run cb

# Run the client with Circuit Breaker Policy
$ dotnet run cb2
```

The endpoint used by the Circuit Breaker can be switched to throw errors by invoking the command:

`curl -X POST http://localhost:5000/api/sayhello -d ""`

## Running Caching demo

```bash
# Console 2
$ cd client-cli

# Run the non-cached client
$ dotnet run cache

# Run the cached client
$ dotnet run cache2
```
