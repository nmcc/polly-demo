# Polly Demo

This repo contains a demo project for using Polly.NET to improve application resilience.

This code is not necessarily production ready and was written for education purposes only.

## Running Retry demo

Run the Web API
```shell
$ cd PollyDemo.Server
$ dotnet run
```

Next, run the console app 
```shell
$ cd PollyDemo.Client
$ dotnet run retry

(Hit Ctrl-C to stop)
```

## Running Fallback demo

Run the Web API
```shell
$ cd PollyDemo.Server
$ dotnet run
```

Next, run the console app 
```shell
$ cd PollyDemo.Client
$ dotnet run fallback
(Hit Ctrl-C to stop)

$ dotnet run fallback2
(Hit Ctrl-C to stop)
```

## Running Circuit Breaker demo

Run the Web API
```shell
$ cd PollyDemo.Server
$ dotnet run
```

Server can be switched to throw errors by invoking the command `curl -X POST http://localhost:5000/api/circuitbreaker -d ""`

Next, run the console app 
```shell
$ cd PollyDemo.Client
$ dotnet run cb
(Hit Ctrl-C to stop)

$ dotnet run cb2
(Hit Ctrl-C to stop)
```

## Running Caching demo

Run the Web API
```shell
$ cd PollyDemo.Server
$ dotnet run
```

Next, run the console app 
```shell
$ cd PollyDemo.Client
$ dotnet run cache
(Hit Ctrl-C to stop)
```

## Running the Web application deomo

Run the Web API
```shell
$ cd PollyDemo.Server
$ dotnet run
```

Server can be switched to throw errors by invoking the command `curl -X POST http://localhost:5000/api/circuitbreaker -d ""`

Next, run the webpp
```shell
$ cd PollyDemo.app
$ dotnet run 
(Hit Ctrl-C to stop)
```