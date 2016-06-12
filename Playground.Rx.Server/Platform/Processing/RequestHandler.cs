namespace Playground.Rx.Server.Platform.Processing
{
    using System;

    using Playground.Rx.Server.Platform.Events;

    public class RequestHandler
    {
        private int eventCounter;

        private readonly RequestAnalyzer analyzer;

        private readonly WebCrawlerEventPublisher webCrawlerEventPublisher;

        public RequestHandler(RequestAnalyzer analyzer, WebCrawlerEventPublisher webCrawlerEventPublisher)
        {
            this.analyzer = analyzer;
            this.webCrawlerEventPublisher = webCrawlerEventPublisher;
        }

        public Response Handle(Request request)
        {
            var analysisResult = this.analyzer.Analyze(request);

            if (analysisResult.AnalysisResultType == RequestAnalysisResultType.WebCrawler)
            {
                this.webCrawlerEventPublisher.Publish(
                    new WebCrawlerDetectedEvent
                        {
                            Id = ++this.eventCounter,
                            CrawlerType = "nurecognized",
                            DateOfEvent = DateTime.Now,
                            TargetedPageUrl = request.Url,
                            Request = request
                        });
            }

            if (analysisResult.AnalysisResultType == RequestAnalysisResultType.FatalError)
            {
                this.webCrawlerEventPublisher.OnError(new Exception("Random error"));
            }

            return new Response();
        }
    }
}