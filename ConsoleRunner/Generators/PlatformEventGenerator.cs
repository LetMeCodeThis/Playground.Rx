namespace ConsoleRunner.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;

    using Playground.Rx.Server.Platform.Processing;
    using Playground.Rx.Server.Utilities;

    public class PlatformEventGenerator
    {
        public static IEnumerable<Request> GenerateBlockingRequests(int requestsCount)
        {
            var requests = new List<Request>(requestsCount);

            for (var i = 0; i < requestsCount; i++)
            {
                requests.Add(new Request
                {
                    Id = i,
                    Url = "http://www.mypoorservice.com/news/{0}".FormatInvariant(i)
                });

                using (new TemporaryConsoleColor(ConsoleColor.DarkMagenta))
                {
                    Console.WriteLine("Request number {0} was generated in thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                }
            }

            return requests;
        }

        public static IEnumerable<Request> GenerateNonBlockingRequests(int requestsCount)
        {
            for (var i = 0; i < requestsCount; i++)
            {
                var request = new Request { Id = i, Url = "http://www.mypoorservice.com/news/{0}".FormatInvariant(i) };

                yield return request;

                using (new TemporaryConsoleColor(ConsoleColor.DarkMagenta))
                {
                    Console.WriteLine("Request number {0} was generated in thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                }
            }
        }
    }
    
    public static class StringExtensions
    {
        public static string FormatInvariant(this string format, params object[] parameter)
        {
            return string.Format(CultureInfo.InvariantCulture, format, parameter);
        }
    }
}