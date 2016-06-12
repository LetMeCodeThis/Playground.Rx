namespace Playground.Rx.Server.Platform.Processing
{
    public class RequestAnalysisResult
    {
        public RequestAnalysisResultType AnalysisResultType { get; set; }
    }

    public enum RequestAnalysisResultType
    {
        Valid,
        Invalid,
        WebCrawler,
        FatalError // NOTE: used to simulate error for WebCrawlerDetectedEventPublisher
    }
}