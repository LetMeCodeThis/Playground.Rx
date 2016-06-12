namespace Playground.Rx.Server.Platform
{
    using Playground.Rx.Server.Platform.Events;
    using Playground.Rx.Server.Platform.Processing;

    public class ServerFactory
    {
        private static readonly RequestAnalyzer RequestAnalyzer = new RequestAnalyzer();

        private static readonly RequestHandler RequestHandler = new RequestHandler(
            RequestAnalyzer,
            Publishers.WebCrawlerEventPublisher);

        public static Server Create()
        {
            var server = new Server(RequestHandler);

            return server;
        }
    }
}