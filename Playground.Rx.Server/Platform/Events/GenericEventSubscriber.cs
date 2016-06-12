namespace Playground.Rx.Server.Platform.Events
{
    using System;

    public abstract class GenericEventSubscriber<TEvent> : IObserver<TEvent>, IDisposable
        where TEvent : PlatformEvent
    {
        private bool isDisposed;

        public string Name { get; protected set; }

        protected IDisposable SubscriptionToken;

        protected GenericEventSubscriber(string name)
        {
            this.Name = name;
        }

        public void SetSubscritionToken(IDisposable subscriptionToken)
        {
            this.SubscriptionToken = subscriptionToken;
        }

        public void Unsubscribe()
        {
            if (this.SubscriptionToken != null)
            {
                this.SubscriptionToken.Dispose();
            }
        }

        public abstract void OnNext(TEvent @event);

        public virtual void OnError(Exception ex) { }

        public virtual void OnCompleted() { }

        public void Dispose()
        {
            if (!this.isDisposed && this.SubscriptionToken != null)
            {
                this.SubscriptionToken.Dispose();
                this.SubscriptionToken = null;
                this.isDisposed = true;
            }
        }
    }
}