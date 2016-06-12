namespace Playground.Rx.Server.Platform.Events
{
    using System;
    using System.Reactive.Subjects;

    using Playground.Rx.Server.Utilities;

    public abstract class GenericEventPublisher<TEvent> : IObservable<TEvent>, IDisposable
        where TEvent : PlatformEvent
    {
        public bool IsDisposed;

        protected Subject<TEvent> Subject;

        public string Name { get; protected set; }

        protected GenericEventPublisher(string name)
        {
            this.Name = name;
            this.Subject = new Subject<TEvent>();
        }

        public IDisposable Subscribe(
            Func<IObservable<TEvent>, IObserver<TEvent>, IDisposable> subscription,
            IObserver<TEvent> observer)
        {
            var subscriber = (GenericEventSubscriber<TEvent>)observer;

            using (new TemporaryConsoleColor(ConsoleColor.Green))
            {
                Console.WriteLine(
                    "Event listener {0} subscribed to event {1} publisher",
                    subscriber.Name,
                    typeof(TEvent).Name);
            }

            return subscription(this.Subject, observer);
        }

        public IDisposable Subscribe(IObserver<TEvent> observer)
        {
            var subscriber = (GenericEventSubscriber<TEvent>)observer;

            using (new TemporaryConsoleColor(ConsoleColor.Green))
            {
                Console.WriteLine(
                    "Event listener {0} subscribed to event {1} publisher",
                    subscriber.Name,
                    typeof(TEvent).Name);
            }

            return this.Subject.Subscribe(observer);
        }

        public virtual void Publish(TEvent eventToPublish)
        {
            this.Subject.OnNext(eventToPublish);
        }

        public virtual void OnError(Exception ex)
        {
            this.Subject.OnError(ex);
        }

        public virtual void OnCompleted()
        {
            this.Subject.OnCompleted();
        }

        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                this.Subject.OnCompleted();
                this.Subject.Dispose();
                this.IsDisposed = true;
            }
        }
    }
}