namespace Playground.Rx.Server.Utilities
{
    using System;
    
    public class TemporaryConsoleColor : IDisposable
    {
        private readonly ConsoleColor previousColor;

        public TemporaryConsoleColor(ConsoleColor color)
        {
            this.previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = this.previousColor;
        }
    }
}