namespace Playground.Rx.Server.Platform
{
    using Playground.Rx.Server.Platform.Processing;

    public class Server
    {
        private readonly RequestHandler requestHandler;

        public Server(RequestHandler requestHandler)
        {
            this.requestHandler = requestHandler;
        }

        public Response Execute(Request request)
        {
            var response = this.requestHandler.Handle(request);

            return response;
        }
    }
}