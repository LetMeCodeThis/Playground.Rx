namespace ConsoleRunner.SetupScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;

    using ConsoleRunner.Generators;

    using Playground.Rx.Client;
    using Playground.Rx.Server.Platform;
    using Playground.Rx.Server.Platform.Events;

    public class ConcurrentWithThreadPoolScenario : Scenario
    {
        public static void Run()
        {
            var requests = PlatformEventGenerator.GenerateNonBlockingRequests(100000);
            var server = ServerFactory.Create();
            var clients = new List<WebCrawlerDetectedEventListener>();

            for (var i = 0; i < 200; i++)
            {
                var client = ClientFactory.CreateWebCrawlerDetectedEventListener("Client {0}".FormatInvariant(i + 1));
                // NOTE: Non blocking subscription
                var subscriptionToken =
                    Publishers.WebCrawlerEventPublisher.Subscribe(
                        (p, s) => p.SubscribeOn(Scheduler.ThreadPool).ObserveOn(Scheduler.ThreadPool).Subscribe(s),
                        client);
                // NOTE: Dispatching subscriber to dedicated task
                //var subscriptionToken =
                //    Publishers.WebCrawlerEventPublisher.Subscribe(
                //        (p, s) => p.SubscribeOn(TaskPoolScheduler.Default).ObserveOn(TaskPoolScheduler.Default).Subscribe(s),
                //        client);

                client.SetSubscritionToken(subscriptionToken);
                clients.Add(client);
            }

            foreach (var request in requests)
            {
                MakeMeBusyForSomeRandomTime();
                server.Execute(request);
            }

            Publishers.WebCrawlerEventPublisher.OnCompleted();
            Publishers.WebCrawlerEventPublisher.Dispose();

            foreach (var c in clients)
            {
                c.Dispose();
            }

            Console.ReadKey(false);
        }
    }
}