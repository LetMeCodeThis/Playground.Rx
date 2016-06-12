namespace Playground.Rx.Server.Platform.Processing
{
    using System;

    public class RequestAnalyzer
    {
        private readonly Random generator;

        public RequestAnalyzer()
        {
            this.generator = new Random();
        }

        public RequestAnalysisResult Analyze(Request request)
        {
            return new RequestAnalysisResult
                       {
                           AnalysisResultType = (RequestAnalysisResultType)this.generator.Next(0, 3)
                       };

            // NOTE: Includes OnError propagation
            //return new RequestAnalysisResult
            //           {
            //               AnalysisResultType =
            //                   this.generator.Next(0, 1000) > 940
            //                       ? RequestAnalysisResultType.FatalError
            //                       : (RequestAnalysisResultType)this.generator.Next(0, 3)
            //           };
        }
    }
}