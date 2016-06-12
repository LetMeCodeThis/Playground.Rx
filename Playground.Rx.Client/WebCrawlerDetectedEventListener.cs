namespace Playground.Rx.Client
{
    using System;
    using System.Threading;

    using Playground.Rx.Server.Platform.Events;
    using Playground.Rx.Server.Utilities;

    public class WebCrawlerDetectedEventListener : GenericEventSubscriber<WebCrawlerDetectedEvent>
    {
        public WebCrawlerDetectedEventListener(string name) : base(name) { }
        
        public override void OnNext(WebCrawlerDetectedEvent @event)
        {
            Console.WriteLine(
                "EventListener {0} handled event {1} while procesing request {2} (thread: {3})",
                this.Name,
                @event.Id,
                @event.Request.Id,
                Thread.CurrentThread.ManagedThreadId);
        }

        public override void OnError(Exception ex)
        {
            using (new TemporaryConsoleColor(ConsoleColor.Red))
            {
                Console.WriteLine(
                    "EventListener {0} handled error {1} (thread {2})",
                    this.Name,
                    ex.Message,
                    Thread.CurrentThread.ManagedThreadId);
            }

            // NOTE: Unsubscribing from event notifications (causes exception?)
            //this.SubscriptionToken.Dispose();
        }

        public override void OnCompleted()
        {
            using (new TemporaryConsoleColor(ConsoleColor.DarkBlue))
            {
                Console.WriteLine(
                    "EventListener {0} was notified that event publisher finished publishing events (thread {1})",
                    this.Name,
                    Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}