# Demo guide - Console application

Run the Web API without request trace logging:
```bash
# Console 1
# Kill the web api process and run it with request tracing disabled
$ cd web-api
$ dotnet run 
```

Run the Web application
```bash
# Console 2
$ cd web-app
$ dotnet run 
```

The endpoint used by the Web Application allows you to introduce a delay or force the server to respond with an error to test the Circuit Breaker.

* **Throw error**: `curl -X POST http://localhost:5000/api/avatar/error -d ""`
* **10 second delay**: `curl -X POST http://localhost:5000/api/avatar/delay -d ""`