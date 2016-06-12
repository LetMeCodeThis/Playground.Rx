namespace ConsoleRunner
{
    using ConsoleRunner.SetupScenarios;

    class Program
    {
        static void Main(string[] args)
        {
            //DefaultScenario.Run();

            // NOTE: subscription on separate thread is not working (probably due to refactoring 
            //       in next versions to utilize platform's most appropriate pool-based scheduler)
            //
            //       Look at process explorer (threads are created) and run in debug mode (untill 
            //       it switches to different task/thread debugger captures OnNext or OnError 
            //       execution and proves clients are receiving messages but after switching
            //       task/thread debugger is not reaching OnNext/OnError and client messages are 
            //       not propagated on the console output
            ConcurrentWithThreadPoolScenario.Run();
        }
    }
}