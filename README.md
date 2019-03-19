# Polly Demo

This repo contains a demo project for using Polly.NET to improve application resilience.

This code is not necessarily production ready and was written for education purposes only.

## Building and running

The applications are written in C# and using ASP .NET Core 2 for the WebApi and Web Application.

You'll need to install DotNet Core 2.2 to build and run this code.

* `web-api` - Web API that serves requests used by the clients
* `client-cli` - Console application that demos the several types of Policies
* `web-app` - Web Application that demonstrates the usage of Policies in a Web Application
* `load-tests` - contains JMeter load test plans to hit the Web Application

## Running the demos

* [Command Line Demos](Demos-Console.md)
* [Web Application Demos](Demos-WebApp.md)
