# Polly Demo

This repo contains a demo project for using Polly.NET to improve application resilience.

This code is not necessarily production ready and was written for education purposes only.

## Building and running

The applications are written in C# and using ASP .NET Core 2 for the WebApi and Web Application.

You'll need to install DotNet Core 2.2 to build and run this code.

* `PollyDemo.Server` - Web API that serves requests used by the clients
* `PollyDemo.Client` - Console application that demos the several types of Policies
* `PollyDemo.App` - Web Application that demonstrates the usage of Policies in a Web Application
* `load-tests` - contains JMeter load test plans to hit the Web Application

## Running the server

The demos require the Web API to be running.

```bash
# Console 1
$ cd PollyDemo.Server
$ dotnet run
```

## Running Retry demo

```bash
# Console 2
$ cd PollyDemo.Client

# Running the non-resilient client. Hit Ctrl-C to stop the applicaton
$ dotnet run retry

# Running the resilient resilient. Hit Ctrl-C to stop the applicaton
$ dotnet run retry2
```

## Running Fallback demo

```bash
# Console 2
$ cd PollyDemo.Client

# Run the client without Fallback Policy
$ dotnet run fallback

# Run the client with Fallback Policy
$ dotnet run fallback2
```

## Running Circuit Breaker demo

```bash
# Console 2 
$ cd PollyDemo.Client

# Run the client without Circuit Breaker Policy
$ dotnet run cb

# Run the client with Circuit Breaker Policy
$ dotnet run cb2
```

The endpoint used by the Circuit Breaker  can be switched to throw errors by invoking the command:

`curl -X POST http://localhost:5000/api/sayhello -d ""`

## Running Caching demo

```bash
# Console 2
$ cd PollyDemo.Client

# Run the non-cached client
$ dotnet run cache

# Run the cached client
$ dotnet run cache2
```

## Running the Web application deomo

```bash
# Console 2
$ cd PollyDemo.app
$ dotnet run 
```

The endpoint used by the Web Application allows you to introduce a delay or force the server to respond with an error to test the Circuit Breaker.

* **Throw error**: `curl -X POST http://localhost:5000/api/avatar/error -d ""`
* **10 second delay**: `curl -X POST http://localhost:5000/api/avatar/delay -d ""`
