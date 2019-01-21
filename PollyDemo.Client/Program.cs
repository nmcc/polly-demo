using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PollyDemo.Client
{
    class Program
    {
        private static string baseUrl;
        private static List<Task> workers;

        static void Main(string[] args)
        {
            ValidateArgs(args);

            if (string.Equals(args[0], "retry", StringComparison.InvariantCultureIgnoreCase))
            {
                Retry.Runner.Run(int.Parse(args[1]));
            }
            else if (string.Equals(args[0], "retry2", StringComparison.InvariantCultureIgnoreCase))
            {
                Retry.ResilientRunner.Run(int.Parse(args[1]));
            }
            else if (string.Equals(args[0], "cb", StringComparison.InvariantCultureIgnoreCase))
            {
                CircuitBreaker.Runner.Run();
            }
            else if (string.Equals(args[0], "cb2", StringComparison.InvariantCultureIgnoreCase))
            {
                CircuitBreaker.ResilientRunner.Run();
            }
        }

        private static void ValidateArgs(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsageAndExit("Missing command");
            }

            if (args[0].StartsWith("retry", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!int.TryParse(args[1], out int iterations))
                {
                    PrintUsageAndExit("Iterations must be a number");
                }
            }
            if (args[0].StartsWith("cb", StringComparison.InvariantCultureIgnoreCase))
            {
                // OK
            }
            else
            {
                PrintUsageAndExit($"Invalid command {args[0]}");
            }
        }

        private static void PrintUsageAndExit(string message)
        {
            Console.Error.WriteLine($"Error: {message}");
            Console.Error.WriteLine();
            Console.Error.WriteLine("Usage: dotnet run <command> <command_args>");
            Console.Error.WriteLine("Supported commands:");
            Console.Error.WriteLine("retry <iterations> - hit the retry endpoint for n times");

            Environment.Exit(1);
        }
    }
}
