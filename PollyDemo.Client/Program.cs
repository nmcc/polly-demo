using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PollyDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ValidateArgs(args);

            if (string.Equals(args[0], "retry", StringComparison.InvariantCultureIgnoreCase))
            {
                Retry.Runner.Run();
            }
            else if (string.Equals(args[0], "retry2", StringComparison.InvariantCultureIgnoreCase))
            {
                Retry.ResilientRunner.Run();
            }
            else if (string.Equals(args[0], "cb", StringComparison.InvariantCultureIgnoreCase))
            {
                CircuitBreaker.Runner.Run();
            }
            else if (string.Equals(args[0], "cb2", StringComparison.InvariantCultureIgnoreCase))
            {
                CircuitBreaker.ResilientRunner.Run();
            }
            else if (string.Equals(args[0], "fallback", StringComparison.InvariantCultureIgnoreCase))
            {
                Fallback.ResilientRunner.Run();
            }
            else if (string.Equals(args[0], "cache", StringComparison.InvariantCultureIgnoreCase))
            {
                Cache.ResilientRunner.Run();
            }
        }

        private static void ValidateArgs(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsageAndExit("Missing command");
            }

            var commandRegex = new Regex("retry2?|cb2?|fallback|cache");

            if (!commandRegex.IsMatch(args[0]))
            {
                PrintUsageAndExit("Invalid command");
            }
        }

        private static void PrintUsageAndExit(string message)
        {
            Console.Error.WriteLine($"Error: {message}");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Usage: dotnet run <command> <command_args>");
            Console.Error.WriteLine("Supported commands:");
            Console.Error.WriteLine("retry - retry demo");
            Console.Error.WriteLine("cb - circuit breaker demo");
            Console.Error.WriteLine("fallback - fallback demo");

            Environment.Exit(1);
        }
    }
}
