namespace ConsoleRunner.SetupScenarios
{
    using System;
    using System.Threading;

    public abstract class Scenario
    {
        private static readonly Random Generator = new Random();

        protected static void MakeMeBusyForSomeRandomTime()
        {
            Thread.Sleep(Generator.Next(50, 250));
        }

        protected static void MakeMeBusyFor(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        protected static void DoHeavyWork()
        {
            var r = 0;

            for (var i = 0; i < 100000; i++)
            {
                r += i;
            }
        }
    }
}