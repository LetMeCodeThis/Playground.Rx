namespace Playground.Rx.Server.Platform.Events
{
    using System;

    using Playground.Rx.Server.Platform.Processing;

    public class WebCrawlerDetectedEvent : PlatformEvent
    {
        public string TargetedPageUrl { get; set; }

        public string CrawlerType { get; set; }

        public DateTime DateOfEvent { get; set; }

        public Request Request { get; set; }
    }
}