namespace ConsoleRunner.SetupScenarios
{
    using System;

    using ConsoleRunner.Generators;

    using Playground.Rx.Client;
    using Playground.Rx.Server.Platform;
    using Playground.Rx.Server.Platform.Events;

    public class DefaultScenario : Scenario
    {
        public static void Run()
        {
            var server = ServerFactory.Create();
            var client = ClientFactory.CreateWebCrawlerDetectedEventListener("default");
            var requests = PlatformEventGenerator.GenerateNonBlockingRequests(1000);
            var subscriptionToken = Publishers.WebCrawlerEventPublisher.Subscribe(client);

            client.SetSubscritionToken(subscriptionToken);

            foreach (var request in requests)
            {
                MakeMeBusyForSomeRandomTime();
                server.Execute(request);
            }

            Publishers.WebCrawlerEventPublisher.OnCompleted();
            Publishers.WebCrawlerEventPublisher.Dispose();
            client.Dispose();

            Console.ReadKey(false);
        }
    }
}