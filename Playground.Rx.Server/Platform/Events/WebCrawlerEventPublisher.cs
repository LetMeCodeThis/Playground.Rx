namespace Playground.Rx.Server.Platform.Events
{
    using System;
    using System.Threading;

    using Playground.Rx.Server.Utilities;

    public class WebCrawlerEventPublisher : GenericEventPublisher<WebCrawlerDetectedEvent>
    {
        public WebCrawlerEventPublisher(string name) : base(name) { }
        
        public override void Publish(WebCrawlerDetectedEvent eventToPublish)
        {
            using (new TemporaryConsoleColor(ConsoleColor.DarkYellow))
            {
                Console.WriteLine(
                    "{0} published an event {1} (thread {2})",
                    this.Name,
                    typeof(WebCrawlerDetectedEvent).Name,
                    Thread.CurrentThread.ManagedThreadId);
            }

            base.Publish(eventToPublish);
        }

        public override void OnError(Exception ex)
        {
            using (new TemporaryConsoleColor(ConsoleColor.DarkRed))
            {
                Console.WriteLine("{0} published an error (thread {1})", this.Name, Thread.CurrentThread.ManagedThreadId);
            }

            base.OnError(ex);
        }

        public override void OnCompleted()
        {
            using (new TemporaryConsoleColor(ConsoleColor.DarkGray))
            {
                Console.WriteLine(
                    "Event publisher {0} finished publishing events (thread {1})",
                    this.Name,
                    Thread.CurrentThread.ManagedThreadId);
            }

            base.OnCompleted();
        }
    }
}