namespace Playground.Rx.Client
{
    public class ClientFactory
    {
        public static WebCrawlerDetectedEventListener CreateWebCrawlerDetectedEventListener(string name)
        {
            return new WebCrawlerDetectedEventListener(name);
        }
    }
}